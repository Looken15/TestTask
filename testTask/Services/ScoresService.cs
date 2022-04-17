using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using testTask.Interfaces;
using testTask.Models.Context;
using testTask.Models.Simple;

namespace testTask.Services
{
    public class ScoresService : IScoresService
    {
        private readonly IScoresRepository v_repository;

        public ScoresService(IScoresRepository repository)
        {
            v_repository = repository;
        }

        public bool ReadExcelWithAdding(string path)
        {
            if (v_repository.GetAllRequirements().Any())
                v_repository.DeleteAllRequirements();

            var excel = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                using (var package = new ExcelPackage(excel))
                {
                    var worksheetCount = package.Workbook.Worksheets.Count;
                    if (worksheetCount == 0)
                        return false;
                    var worksheet = package.Workbook.Worksheets[0];
                    var columnsCount = worksheet.Dimension.End.Column;
                    var currentPriority = 1;
                    for (var i = 2; i < worksheet.Dimension.End.Row + 1; ++i)
                    {
                        var req = new SpecialtyRequirements();

                        var code = worksheet.Cells[i, 1].Value?.ToString();
                        req.SpecialtyCode = code;

                        var kinds = worksheet.Cells[i, 2].Value?.ToString().Split(';');
                        foreach (var kind in kinds)
                            req.EducationKind.Add(v_repository.GetEducationKind(kind));

                        var subj = worksheet.Cells[i, 3].Value?.ToString();
                        req.Subject = v_repository.GetSubject(subj);

                        var replSubj = worksheet.Cells[i, 4].Value?.ToString();
                        if (replSubj == null)
                            req.ReplaceSubject = null;
                        else
                            req.ReplaceSubject = v_repository.GetSubject(replSubj);

                        var forms = worksheet.Cells[i, 5].Value?.ToString().Split(';');
                        foreach (var form in forms)
                            req.TestForm.Add(v_repository.GetTestForm(form));

                        int.TryParse(worksheet.Cells[i, 6].Value?.ToString(), out int score);
                        req.MinScore = score;

                        var similarReqs = v_repository.GetRequirements(code, kinds).ToList();
                        if (similarReqs.Count == 0)
                        {
                            req.Priority = currentPriority = 1;
                        }
                        else
                        {
                            if (req.ReplaceSubject == null)
                            {
                                req.Priority = ++currentPriority;
                            }
                            else
                            {
                                req.Priority = currentPriority;
                            }
                        }

                        v_repository.Add(req);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
