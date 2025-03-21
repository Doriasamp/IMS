﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IMS.CoreBusiness;
public class ProductTransaction
{

    public int ProductTransactionId { get; set; }

    public string SONumber { get; set; } = string.Empty;
    public string ProductionNumber { get; set; } = string.Empty;
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int QuantityBefore { get; set; }
    [Required]
    public ProductTransactionType ActivityType { get; set; }
    [Required]
    public int QuantityAfter { get; set; }
    [Required]
    public double? UnitPrice { get; set; }
    [Required]
    public DateTime TransactionDate { get; set; }
    [Required]
    public string DoneBy { get; set; } = string.Empty;

    [JsonIgnore]
    public Product? Product { get; set; }   // Navigation property
}