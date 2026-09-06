using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCOFIN.Infrastructure;

[Table("Obi_Code_Entry")]
//[Index("RowId", Name = "IX_Obi_Code_Entry", IsUnique = true)]
public partial class ObiCodeEntry
{
    [Column("Row_ID")]
    public long RowId { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string Type { get; set; } = null!;

    [Column("New_ArticleNo")]
    [StringLength(25)]
    [Unicode(false)]
    public string NewArticleNo { get; set; } = null!;

    [Column("Parent_Code")]
    [StringLength(15)]
    [Unicode(false)]
    public string ParentCode { get; set; } = null!;

    [Key]
    [Column("Article_No")]
    [StringLength(25)]
    [Unicode(false)]
    public string ArticleNo { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Des { get; set; } = null!;

    [Column("Dra_no")]
    [StringLength(100)]
    [Unicode(false)]
    public string DraNo { get; set; } = null!;

    [Column("Tech_DraNo")]
    [StringLength(150)]
    [Unicode(false)]
    public string? TechDraNo { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Active { get; set; }

    [Column("Update_Code")]
    [StringLength(1)]
    [Unicode(false)]
    public string? UpdateCode { get; set; }

    [Column("ItemGroup_Code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ItemGroupCode { get; set; }

    [Column("ItemMain_Code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ItemMainCode { get; set; }

    [Column("SubContract_Item")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SubContractItem { get; set; }

    [Column("ItemClassification_Code")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ItemClassificationCode { get; set; }

    [Column("Item_Make")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ItemMake { get; set; }

    [Column("ArticleCode_PO")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ArticleCodePo { get; set; }

    [Column("Ext_ArticleCodePO")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ExtArticleCodePo { get; set; }

    [Column("Ext_ArticleCode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? ExtArticleCode { get; set; }

    [Column("Entry_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EntryBy { get; set; }

    [Column("Entry_Date", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("Client_Name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [Column("MAC_Address")]
    [StringLength(15)]
    [Unicode(false)]
    public string? MacAddress { get; set; }

    [Column("Entered_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? EnteredBy { get; set; }

    [Column("Entered_Date", TypeName = "datetime")]
    public DateTime? EnteredDate { get; set; }

    [Column("Modified_By")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ModifiedBy { get; set; }

    [Column("Modified_Date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("Drawing_No")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DrawingNo { get; set; }

    [Column("For_Machine")]
    [StringLength(1)]
    [Unicode(false)]
    public string? ForMachine { get; set; }

    [Column("Main_ProdArticleCode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MainProdArticleCode { get; set; }

    [Column("Main_CompArticleCode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MainCompArticleCode { get; set; }

    [Column("Main_SFArticleCode")]
    [StringLength(25)]
    [Unicode(false)]
    public string? MainSfarticleCode { get; set; }

    [Column("LastUpdated_Date", TypeName = "datetime")]
    public DateTime LastUpdatedDate { get; set; }
}
