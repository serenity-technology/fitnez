namespace Fitnez;

public record Invoice
{
    public DateOnly Date {  get; init; }
    public List<InvoiceDetail> Details { get; init; } = default!;
}