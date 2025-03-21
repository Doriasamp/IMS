﻿
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IMS.CoreBusiness;
public class InventoryTransaction
{

    public int InventoryTransactionId { get; set; }

    public string PoNumber { get; set; } = string.Empty;
    public string ProductionNumber { get; set; } = string.Empty;
    [Required]
    public int InventoryId { get; set; }
    [Required]
    public int QuantityBefore { get; set; }
    [Required]
    public InventoryTransactionType ActivityType { get; set; }
    [Required]
    public int QuantityAfter { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    [Required]
    public DateTime TransactionDate { get; set; }
    [Required]
    public string DoneBy { get; set; } = string.Empty;

    [JsonIgnore]
    public Inventory? Inventory { get; set; }   // Navigation property
}
