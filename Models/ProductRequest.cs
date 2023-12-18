namespace Productos.Models;

public record ProductRequest(
     string ProductName,
     DateTime ValidUntilDate,
     decimal Cost,
     int Avaliability
 );
