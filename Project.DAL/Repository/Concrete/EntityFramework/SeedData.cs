
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DAL.Repository.Concrete.EntityFramework
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<KuskonmazContext>();

                context.Database.Migrate();
                if (!context.Posts.Any())
                {
                    var posts = new[]
                    {
                    new Post(){ postTitle="Limonata", postDescription="Püf Notaları ile Limonata Yapımı", postImage="food1.jpg", postContent="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",dateAdded=DateTime.Now.AddDays(-10)},
                    new Post(){ postTitle="Hamburger", postDescription="Barbekü soslu Hamburger",postImage="food2.jpg",postContent="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum" ,dateAdded=DateTime.Now.AddDays(-20)},
                    new Post(){ postTitle="Sezar Salata", postDescription="Az Kalorili Sezar Salata ",postImage="food3.jpg",postContent="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",dateAdded=DateTime.Now.AddDays(-30)},
                    new Post(){ postTitle="limonlu Cheesecake", postDescription="En İyi Cheesecake",postImage="food4.jpg", postContent="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",dateAdded=DateTime.Now.AddDays(-40)},
                    new Post(){ postTitle="Çilekli Milkshake", postDescription="Yoğun Çilekli Milkshake",postImage="food4.jpg", postContent="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",dateAdded=DateTime.Now.AddDays(-40)},
                    new Post(){ postTitle="Çilekli Cheesecake", postDescription="Ekşi Çilekli Cheesecake",postImage="food1.jpg", postContent="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum",dateAdded=DateTime.Now.AddDays(-40)}
                    };
                    context.Posts.AddRange(posts);

                    var category = new[]
                    {
                    new Category(){ categoryName="Drinks"},
                    new Category(){ categoryName="Fastfood"},
                    new Category(){ categoryName="Diet"},
                    new Category(){ categoryName="Deserts"},
                    new Category(){ categoryName="Healty"}
                     };
                    context.Categories.AddRange(category);

                    var postcategories = new[]
                    {

                    new PostCategory(){Post=posts[0] , Category=category[0] },
                    new PostCategory(){Post=posts[1] , Category=category[1] },
                    new PostCategory(){Post=posts[3] , Category=category[2] },
                    new PostCategory(){Post=posts[2] , Category=category[3] },
                    new PostCategory(){Post=posts[4] , Category=category[4] },
                     new PostCategory(){Post=posts[5] , Category=category[3] }
                     };
                    context.AddRange(postcategories);

                    var tag = new[]
                    {
                    new Tag(){ tagName="lemon"},
                    new Tag(){ tagName="meat"},
                    new Tag(){ tagName="salad"},
                    new Tag(){ tagName="cake"},
                     new Tag(){ tagName="strawberry"}
                     };
                    context.Tags.AddRange(tag);

                    var posttags = new[]
                    {

                    new PostTag(){Post=posts[0] , Tag=tag[0] },
                    new PostTag(){Post=posts[1] , Tag=tag[1] },
                    new PostTag(){Post=posts[3] , Tag=tag[2] },
                    new PostTag(){Post=posts[2] , Tag=tag[3] },
                    new PostTag(){Post=posts[4] , Tag=tag[4] },
                     new PostTag(){Post=posts[5] , Tag=tag[4] }
                     };
                    context.AddRange(posttags);

                    var images = new[]
                    {
                        new Image(){imageName="food1.jpg", Post=posts[0]},
                        new Image(){imageName="food2.jpg", Post=posts[1]},
                        new Image(){imageName="food3.jpg", Post=posts[2]},
                        new Image(){imageName="food4.jpg", Post=posts[3]},
                        new Image(){imageName="food4.jpg", Post=posts[4]},
                        new Image(){imageName="food1.jpg", Post=posts[5]},


                        //new Image(){imageName="product1.jpg", Product=products[1]},
                        //new Image(){imageName="product2.jpg", Product=products[1]},
                        //new Image(){imageName="product3.jpg", Product=products[1]},
                        //new Image(){imageName="product4.jpg", Product=products[1]},

                        //new Image(){imageName="product1.jpg", Product=products[2]},
                        //new Image(){imageName="product2.jpg", Product=products[2]},
                        //new Image(){imageName="product3.jpg", Product=products[2]},
                        //new Image(){imageName="product4.jpg", Product=products[2]},

                        //new Image(){imageName="product1.jpg", Product=products[3]},
                        //new Image(){imageName="product2.jpg", Product=products[3]},
                        //new Image(){imageName="product3.jpg", Product=products[3]},
                        //new Image(){imageName="product4.jpg", Product=products[3]}
                    };
                    context.Images.AddRange(images);
                    context.SaveChanges();
                }

                if (!context.Abouts.Any())
                {
                    var abouts = new[]
                    {
                        new About(){ aboutContent="Where can I get somevariations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which dont look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isnt anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc."}
                    };
                    context.Abouts.AddRange(abouts);
                    context.SaveChanges();
                }

                if (!context.Contacts.Any())
                {
                    var contact = new[]
                    {
                        new Contact(){ contactAddress="3919 New Cut Rd, Mary land, United State.", contactPhonemumber="(12)-100-100-100", contactFax="(12)-112-123-123", contactMail="no@gmail.com", facebook="https://www.facebook.com/", instagram="https://www.instagram.com/" , twitter="https://twitter.com/" ,pinterest="https://tr.pinterest.com/"}
                    };
                    context.Contacts.AddRange(contact);
                    context.SaveChanges();
                }
                if (!context.Admins.Any())
                {
                    var admin = new[]
                    {
                        new Admin(){ adminName="nigarozluk", adminpassword="123456", adminMail="nigarozluk@gmail.com"}
                    };
                    context.Admins.AddRange(admin);
                    context.SaveChanges();
                }
            }
        }
    }
}
