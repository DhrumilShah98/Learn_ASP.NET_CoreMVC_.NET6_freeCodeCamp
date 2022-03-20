using Microsoft.AspNetCore.Mvc;
using BulkyBookWebApp.Data;
using BulkyBookWebApp.Models;

/**
 * Package: BulkyBookWebApp.Controllers
 */
namespace BulkyBookWebApp.Controllers
{
    /**
     * CategoryController class (Location - BulkyBookWebApp/Controllers/CategoryController.cs) handles the user request 
     * and acts as an interface between Category model (Location - BulkyBookWebApp/Models/Category.cs) and 
     * Category views (Location - BulkyBookWebApp/Views/Category/*.cshtml).
     */
    public class CategoryController : Controller
    {
        /**
         * ApplicationDbContext object (File - BulkyBookWebApp/Data/ApplicationDbContext.cs) for database operations.
         */
        private readonly ApplicationDbContext _db;

        /**
         * Constructor to initialize this class.
         */
        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }

        /**
         * GET
         * Get all categories from database and pass it to the view.
         * Controller: Category
         * Action: Index
         * URL: GET Domain/Category/Index
         */
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        /**
         * GET
         * Get a form page to create a new category.
         * Controller: Category
         * Action: Create
         * URL: GET Domain/Category/Create
         */
        public IActionResult Create()
        {
            return View();
        }

        /**
         * POST
         * Post a category to the database.
         * Controller: Category
         * Action: Create
         * URL: POST Domain/Category/Create
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid) 
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /**
         * GET
         * Get a form page to edit an existing category.
         * Controller: Category
         * Action: Edit
         * Route: id
         * URL: GET Domain/Category/Edit/id
         */
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        /**
         * POST
         * Post the updated category to the database.
         * Controller: Category
         * Action: Edit
         * URL: POST Domain/Category/Edit
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /**
          * DELETE
          * Delete an existing category.
          * Controller: Category
          * Action: Delete
          * Route: id
          * URL: GET Domain/Category/Delete/id
          */
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            _db.Categories.Remove(_db.Categories.Single(u => u.Id == id));
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}