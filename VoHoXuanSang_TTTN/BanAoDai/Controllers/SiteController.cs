using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;

namespace BanAoDai.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        private BanAoDaiDBContext db = new BanAoDaiDBContext();
        public ActionResult Index(string slug = null)
        {
            if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = db.Links.Where(x => x.Slug == slug).FirstOrDefault();
                if (link != null)

                {
                    var typelink = link.TypeLink;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug);
                            }
                        case "brand":
                            {
                                return this.ProductBrand(slug);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }

                    }
                }
                else
                {
                    Product product = db.Products.Where(x => x.Slug == slug && x.Status == 1).FirstOrDefault();
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = db.Posts.Where(x => x.Slug == slug && x.Status == 1 && x.PostType == "post").FirstOrDefault();
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }

                }
            }

            return this.Error404(slug);
        }
        public ActionResult Home()
        {
            var list = db.Categorys.Where(x => x.ParentId == 0 && x.Status == 1).ToList();
            return View("Home", list);
        }
        public ActionResult ProductHome(int catid)
        {
            var listcatid = new List<int>();
            listcatid.Add(catid);
            var categorys = db.Categorys.Where(x => x.ParentId == catid && x.Status == 1).ToList();
            if (categorys.Count > 0)
            {
                foreach (var cat in categorys)
                {
                    listcatid.Add(cat.Id);
                    var categorys1 = db.Categorys.Where(x => x.ParentId == cat.Id && x.Status == 1).ToList();
                    if (categorys1.Count > 0)
                    {
                        foreach(var cat1 in categorys1)
                        {

                            listcatid.Add(cat1.Id);
                        }
                    }
                }
            }
            var list = db.Products.Where(x => x.Status == 1 && listcatid.Contains(x.CatId))
                .OrderByDescending(x=>x.CreatedBy).Take(5).ToList();
            return View("ProductHome", list);
        }
        public ActionResult ProductCategory(string slug)
        {
            var category=db.Categorys.Where(x=>x.Status==1&& x.Slug==slug).FirstOrDefault();
            int catid = category.Id;
            var listcatid = new List<int>();
            listcatid.Add(catid);
            var categorys = db.Categorys.Where(x => x.ParentId == catid && x.Status == 1).ToList();
            if (categorys.Count > 0)
            {
                foreach (var cat in categorys)
                {
                    listcatid.Add(cat.Id);
                    var categorys1 = db.Categorys.Where(x => x.ParentId == cat.Id && x.Status == 1).ToList();
                    if (categorys1.Count > 0)
                    {
                        foreach (var cat1 in categorys1)
                        {

                            listcatid.Add(cat1.Id);
                        }
                    }
                }
            }
            var products=db.Products.Where(x=>x.Status==1 && listcatid.Contains(x.CatId)).OrderByDescending(x=>x.CreatedBy)
                .ToList();
            return View("ProductCategory",products);
        }
        public ActionResult ProductBrand(string slug)
        {
            var brand = db.Brands.Where(x => x.Status == 1 && x.Slug == slug).FirstOrDefault();
            var products=db.Products.Where(x=>x.Status==1 && x.BrandId==brand.Id).OrderByDescending(x => x.CreatedBy).ToList();
            return View("ProductBrand");
        }
        public ActionResult ProductDetail(Product product)
        {
            int catid = product.CatId;
            var listcatid = new List<int>();
            listcatid.Add(catid);
            var categorys = db.Categorys.Where(x => x.ParentId == catid && x.Status == 1).ToList();
            if (categorys.Count > 0)
            {
                foreach (var cat in categorys)
                {
                    listcatid.Add(cat.Id);
                    var categorys1 = db.Categorys.Where(x => x.ParentId == cat.Id && x.Status == 1).ToList();
                    if (categorys1.Count > 0)
                    {
                        foreach (var cat1 in categorys1)
                        {

                            listcatid.Add(cat1.Id);
                        }
                    }
                }
            }
            var product_other = db.Products.Where(x => x.Status == 1 && x.Id!=product.Id && listcatid.Contains(x.CatId))
                .OrderByDescending(x => x.CreatedBy).Take(5).ToList();
            ViewBag.ListOrther= product_other;
            return View("ProductDetail", product);
        }
        public ActionResult PostTopic(string slug)
        {
            var topic = db.Topics.Where(x => x.Status == 1 && x.Slug == slug).FirstOrDefault();
            var posts = db.Posts.Where(x => x.Status == 1 && x.PostType=="post"&& x.TopicId == topic.Id)
                .OrderByDescending(x => x.CreatedBy).ToList();
            return View("PostTopic", posts);
        }
        public ActionResult PostPage(string slug)
        {
            var page = db.Posts.Where(x => x.Status == 1 && x.PostType == "page" && x.Slug == slug)
               .FirstOrDefault();
            return View("PostPage", page);
        }
        public ActionResult PostDetail(Post post)
        {
           
            var post_orther=db.Posts.Where(x=>x.Status==1 && x.Id!=post.Id && x.TopicId== post.TopicId && x.PostType=="post")
                .OrderByDescending(x=>x.CreatedBy).Take(5).ToList();
            ViewBag.Post_orther= post_orther;
            return View("PostDetail", post);
        }
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }

    }
}