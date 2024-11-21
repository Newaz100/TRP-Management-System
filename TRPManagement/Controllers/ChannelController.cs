using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TRPManagement.DTOs;
using TRPManagement.EF;

namespace TRPManagement.Controllers
{
    public class ChannelController : Controller
    {
        private TRMdbEntities db = new TRMdbEntities();
 
        // GET: ChannelList
        public ActionResult ChannelList()
        {
            var channels = db.Channels.ToList();
            return View(channels);
        }

        // GET: ChannelCreate
        public ActionResult ChannelCreate()
        {
            var model = new ChannelDTO(); // Initialize an empty model
            return View(model);
        }

        // POST: ChannelCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChannelCreate(ChannelDTO model)
        {
            if (ModelState.IsValid)
            {
                // Save the channel data to the database
                var newChannel = new Channel
                {
                    ChannelName = model.ChannelName,
                    EstablishedYear = model.EstablishedYear,
                    Country = model.Country
                };

                db.Channels.Add(newChannel);
                db.SaveChanges();

                TempData["Success"] = "Channel created successfully!";
                return RedirectToAction("ChannelList");
            }

            // If validation fails, return the same view with the model to show errors
            return View(model);
        }


        // GET: ChannelEdit
        public ActionResult ChannelEdit(int id)
        {
            var channel = db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }

            var dto = new ChannelDTO
            {
                ChannelId = channel.ChannelId,
                ChannelName = channel.ChannelName,
                EstablishedYear = channel.EstablishedYear,
                Country = channel.Country
            };

            return View(dto);
        }

        // POST: ChannelEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChannelEdit(ChannelDTO dto)
        {
            if (ModelState.IsValid)
            {
                var channel = db.Channels.Find(dto.ChannelId);
                if (channel == null)
                {
                    return HttpNotFound();
                }

                channel.ChannelName = dto.ChannelName;
                channel.EstablishedYear = dto.EstablishedYear;
                channel.Country = dto.Country;

                db.SaveChanges();
                TempData["Success"] = "Channel updated successfully!";
                return RedirectToAction("ChannelList");
            }
            TempData["Error"] = "Failed to update channel. Please check the input.";
            return View(dto);
        }

        // GET: Delete

        public ActionResult Delete(int id)
        {
            var channel = db.Channels.Find(id);
            if (channel == null)
            {
                return HttpNotFound();
            }

            if (channel.Programs.Any())
            {
                TempData["Error"] = "Cannot delete a channel with associated programs.";
                return RedirectToAction("ChannelList");
            }

            return View(channel);
        }

        // POST: DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var channel = db.Channels.Find(id); // Assuming you're using Entity Framework
            if (channel != null)
            {
                // Check if there are any associated programs
                if (db.Programs.Any(p => p.ChannelId == id))
                {
                    TempData["Error"] = "This channel has associated programs and cannot be deleted.";
                    return RedirectToAction("ChannelList");
                }

                db.Channels.Remove(channel);
                db.SaveChanges();
                TempData["Success"] = "Channel deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Channel not found.";
            }

            return RedirectToAction("ChannelList");
        }



    }
}