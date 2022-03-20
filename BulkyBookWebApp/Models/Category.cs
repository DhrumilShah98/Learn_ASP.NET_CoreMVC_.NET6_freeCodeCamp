using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/**
 * Package: BulkyBookWebApp.Models
 */
namespace BulkyBookWebApp.Models
{
    /**
     * Category class (Location - BulkyBookWebApp/Models/Category.cs) stores a category.
     * Migration Command: add-migration AddCategoryToDatabase
     * Update Command: update-database
     */
    public class Category
    {
        /**
         * Description: Id of this Category.
         * Constraints: Primary Key, Identity Column, Required
         */
        [Key]
        public int Id { get; set; }

        /**
         * Description: Name of this Category.
         * Constraints: Required
         */
        [Required]
        public string Name { get; set; } = "";

        /**
         * Description: Display order of this Category.
         */
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be between 1 and 100 only!!")]
        public int DisplayOrder { get; set; }

        /**
         * Description: Date and time of this Category creation.
         */
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}