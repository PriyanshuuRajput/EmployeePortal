using Application.Dto;
using ClosedXML.Excel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace EmployeePortal.Services
{
    public class ExportService
    {
        public byte[] ExportPdfService(List<EmployeeDto> emp)
        {
            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph("Employee Report"));

            Table table = new Table(8);
            table.AddHeaderCell("EmpCode");
            table.AddHeaderCell("Name");
            table.AddHeaderCell("Email");
            table.AddHeaderCell("Mobile");
            table.AddHeaderCell("Hire Date");
            table.AddHeaderCell("Department");
            table.AddHeaderCell("Designation");
            table.AddHeaderCell("Salary");

            foreach (var employee in emp)
            {
                table.AddCell(employee.EmpCode);
                table.AddCell($"{employee.FirstName} {employee.LastName}");
                table.AddCell(employee.Email);
                table.AddCell(employee.PhoneNumber);
                table.AddCell(employee.HireDate?.ToString("dd MMM yyyy"));
                table.AddCell(employee.Department?.Name);
                table.AddCell(employee.Designation?.Name);
                table.AddCell(employee.Salary.ToString());

            }
            document.Add(table);
            document.Close();

            return stream.ToArray();

        }

        public byte[] ExportExcelService(List<EmployeeDto> employees)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Employees");

            ws.Cell(1, 1).Value = "EmpCode";
            ws.Cell(1, 2).Value = "Name";
            ws.Cell(1, 3).Value = "Email";
            ws.Cell(1, 4).Value = "Mobile";
            ws.Cell(1, 5).Value = "Hire Date";
            ws.Cell(1, 6).Value = "Department";
            ws.Cell(1, 7).Value = "Designation";
            ws.Cell(1, 8).Value = "Salary";

            int row = 2;

            foreach (var emp in employees)
            {
                ws.Cell(row, 1).Value = emp.EmpCode;
                ws.Cell(row, 2).Value = $"{emp.FirstName} {emp.LastName}";
                ws.Cell(row, 3).Value = emp.Email;
                ws.Cell(row, 4).Value = emp.PhoneNumber;
                ws.Cell(row, 5).Value = emp.HireDate?.ToString("dd MMM yyyy");
                ws.Cell(row, 6).Value = emp.Department?.Name;
                ws.Cell(row, 7).Value = emp.Designation?.Name;
                ws.Cell(row, 8).Value = emp.Salary;

                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
