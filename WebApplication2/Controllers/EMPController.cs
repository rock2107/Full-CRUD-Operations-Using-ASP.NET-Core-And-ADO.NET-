using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApplication2.DAL;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EMPController : Controller
    {
        private readonly EMP_DAL _dal;
        public EMPController(EMP_DAL dal)
        {
            _dal = dal;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _dal.GetAll();

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(employees);
        }
        //[HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            try

            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "model data is invalid";
                }
                bool result = _dal.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = "unable to save data";
                    return View();
                }
                TempData["SuccessMessage"] = "employee details is saved";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Employee employee = _dal.GetbyId(id);
                if (employee.Id == 0)
                {
                    TempData["errorMassage"] = $"Employee details not found with Id :{id}";
                    return RedirectToAction("Index");
                }
                return View(employee);


            }
            catch (Exception ex)
            {
                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee Model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    TempData["errorMassage"] = "Model data is invalid";
                    return View();

                }
                bool result = _dal.Update(Model);
                if (!result)
                {

                    TempData["errorMessage"] = "unable to save data";
                    return View();
                }
                TempData["SuccessMessage"] = "employee details is saved";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Employee employee = _dal.GetbyId(id);
                if (employee.Id == 0)
                {
                    TempData["errorMassage"] = $"Employee details not found with Id :{id}";
                    return RedirectToAction("Index");
                }
                return View(employee);


            }
            catch (Exception ex)
            {
                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(Employee model) 
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    bool result = _dal.Delete(model.Id);
                    if (!result)
                    {

                            TempData["errorMessage"] = "unable to save data";
                            return View();
                    }

                }
      
               
                TempData["SuccessMessage"] = "employee details is saved";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
    }
}
