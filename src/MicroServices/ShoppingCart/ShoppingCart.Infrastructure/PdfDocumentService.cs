using ShoppingCart.Domain.Abstractions;

namespace ShoppingCart.Infrastructure;

public class PdfDocumentService : IDocumentService
{
    public byte[] Generate()
    {
        Console.WriteLine("Generating...");

        return null;
    }
}


    