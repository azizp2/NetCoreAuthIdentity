using api.Models;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Linq.Expressions;

namespace api.Services
{
    public class InvoiceRenderingService
    {
        public InvoiceRenderingService()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        }
        public byte[] GeneratedInvoicePdf(Invoice invoice)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.Background(Colors.White);
                    page.Header()
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .Column(column =>
                                {
                                    column.Item().Text("COMPANY NAME").FontFamily("Arial").FontSize(28).Bold();
                                    column.Item().Text("COMPANY ADDRESS").FontFamily("Arial");
                                });

                            row.RelativeItem()
                                .ShowOnce()
                                .Text("INVOICE")
                                .AlignRight()
                                .FontFamily("Arial")
                                .ExtraBlack()
                                .FontSize(30);


                        });


                    page.Content()
                     .PaddingTop(50)
                     .Column(column =>
                     {
                         column.Item().Row(row =>
                         {
                             row.RelativeItem()
                                 .Column(column2 =>
                                 {
                                     column2.Item()
                                         .Text("Bill To:")
                                         .Bold();
                                     column2.Item()
                                     .Text(invoice.Client?.ClientName)
                                     .FontFamily("Arial")
                                     .FontSize(15)
                                     .Bold();
                                     column2.Item().Text(invoice.Client?.ClientAddress);
                                 });

                             row.RelativeItem().Column(column2 =>
                             {
                                 column2.Item()
                                     .Text($"Invoice #:{invoice.InvoiceNumber}")
                                     .AlignRight()
                                     .Bold();

                                 column2.Item()
                                     .PaddingTop(2)
                                     .Text($"Date : {invoice.InvoiceDate:dd-MM-yyyy}")
                                     .AlignRight();


                             });

                             column.Item()
                             .PaddingTop(40).Table(table =>
                             {
                                 table.ColumnsDefinition(columns =>
                                 {
                                     columns.ConstantColumn(40);
                                     columns.RelativeColumn(); // Description
                                     columns.ConstantColumn(50); // Quantity
                                     columns.ConstantColumn(60); // Quantity
                                     columns.ConstantColumn(70); // Quantity
                                 });

                                 table.Header(header =>
                                 {
                                     header.Cell().Text("#");
                                     header.Cell().Text("Description");
                                     header.Cell().AlignRight().Text("Qty").Bold();
                                     header.Cell().AlignRight().Text("Price").AlignRight();
                                     header.Cell().AlignRight().Text("Total").AlignRight();

                                     header.Cell()
                                        .ColumnSpan(5) 
                                        .PaddingVertical(5)
                                        .BorderBottom(1)
                                        .BorderColor(Colors.Black);
                                 });

                                 for (var i = 0; i < invoice.InvoiceItems.Count; i++)
                                 {
                                     var item = invoice.InvoiceItems[i];
                                     var bgColor = i % 2 == 0 ? Colors.White : Colors.Grey.Lighten3;

                                     // Kolom No
                                     table.Cell()
                                          .Background(bgColor)
                                          .Padding(4)
                                          .AlignCenter()
                                          .Text((i + 1).ToString());

                                     // Deskripsi
                                     table.Cell()
                                          .Background(bgColor)
                                          .Padding(4)
                                          .Text(item.Description);

                                     // Quantity
                                     table.Cell()
                                          .Background(bgColor)
                                          .Padding(4)
                                          .AlignRight()
                                          .Text(item.Quantity.ToString("N2"));

                                     // Harga Satuan
                                     table.Cell()
                                          .Background(bgColor)
                                          .Padding(4)
                                          .AlignRight()
                                          .Text(item.UnitPrice.ToString("N2"));

                                     // Subtotal
                                     var subtotal = item.UnitPrice * item.Quantity;
                                     table.Cell()
                                          .Background(bgColor)
                                          .Padding(4)
                                          .AlignRight()
                                          .Text(subtotal.ToString("N2"));
                                 }

                                 table.Cell().ColumnSpan(5)
                                    .PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Black);
                                 table.Cell().ColumnSpan(4).Text("Grand Total").Bold().AlignRight();
                                 var total = invoice.InvoiceItems.Sum(x => x.UnitPrice * x.Quantity);

                                 table.Cell()
                                      .AlignRight()
                                      .Text(total.ToString("N2"));


                                 column.Item().Column(columns =>
                                 {
                                     column.Item()
                                        .PaddingTop(30)
                                        .Text("Thank you for your busineess.")
                                        .FontSize(15)
                                        .Bold();
                                 });

                             });
                         });

                     });


                    page.Footer().Column(column =>
                    {
                        column.Item().PaddingVertical(10).Text(text =>
                        {
                            text.Span("Page ");
                            text.CurrentPageNumber();
                            text.Span(" of ");
                            text.TotalPages();
                            text.AlignCenter();
                        });
                    });
                });
            });

            // Companion hanya untuk debugging, bisa di-comment kalau perlu
             //document.ShowInCompanion();

            return document.GeneratePdf();
        }

    }
}
