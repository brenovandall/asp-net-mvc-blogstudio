﻿using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammerStudio.Web.Data;
using ProgrammerStudio.Web.Models.Domain;
using ProgrammerStudio.Web.Models.ViewModels;

namespace ProgrammerStudio.Web.Controllers;

[Authorize(Roles = "admin")]
public class AdminBlogsController : Controller
{
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public AdminBlogsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var tags = await _context.Tags.ToListAsync(); // returns a list of tags

        AddBlogModel blog = new()
        {
            Tags = tags.Select(x => new SelectListItem{ Text = x.DisplayName, Value = x.Id.ToString() }) // using this to list the tags on the field
        };

        return View(blog);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddBlogModel addBlogModel)
    {
        // mapping the addBlogModel view-model to a blog post
        var newBlogPost = _mapper.Map<BlogPost>(addBlogModel);

        List<BlogTag> listOfTags = new(); // this is the list that will have all the tags selected
        foreach (var item in addBlogModel.SelectedTags) // loop on the selected tags
        {
            var selectedTag = Guid.Parse(item); // the id of addBlogModel selected tag
            var tagFromDatabase = await _context.Tags.FirstOrDefaultAsync(x => x.Id == selectedTag); // getting the the tag that has the same selectedTag id

            if (tagFromDatabase != null)
            {
                listOfTags.Add(tagFromDatabase); // add to the list of tags the existing tag from database
            }
        }

        newBlogPost.BlogTags = listOfTags; // taking the mapped blog, the collection of tags will be the tag list "listOfTags"

        await _context.Posts.AddAsync(newBlogPost);
        await _context.SaveChangesAsync();

        return RedirectToAction("Add");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var posts = await _context.Posts.Include(x => x.BlogTags).ToListAsync();

        return View(posts);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var post = await _context.Posts.Include(x => x.BlogTags).FirstOrDefaultAsync(x => x.Id == id); // all the posts

        var tags = await _context.Tags.ToListAsync(); // all the tags for selecting tags field


        /* 
         * ***************************************
         * THIS CODE IS NOT ON USE IN THIS CASE 
         * *************************************** */
        // List<BlogTag> tags = new();

        //string[] tagsArray = new string[post.BlogTags.Count];
        //int index = 0;

        //foreach(var item in post.BlogTags)
        //{
        //    tagsArray.Append(item.Name);
        //    index++;
        //}

        //foreach (var item in post.BlogTags)
        //{
        //    tags.Add(item);
        //}

        if (post != null)
        {
            EditBlogModel model = new()
            {
                Id = post.Id,
                Heading = post.Heading,
                Title = post.Title,
                Content = post.Content,
                Description = post.Description,
                ImageUrl = post.ImageUrl,
                UrlHandle = post.UrlHandle,
                PublishDate = post.PublishDate,
                Author = post.Author,
                Visible = post.Visible,
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() }), // showing all the tags on select items 
                SelectedTags = post.BlogTags.Select(x => x.Id.ToString()).ToArray() // selected tags are displaying here, is an array of tags that user choose
            };

            //model.BlogTags = tags;

            return View(model);
        }

        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditBlogModel editBlogModel)
    {
        // this is the blog post that user is looking for update -- >
        var oldBlogPost = await _context.Posts.Include(x => x.BlogTags).FirstOrDefaultAsync(x => x.Id == editBlogModel.Id);

        BlogPost post = new();

        List<BlogTag> tags = new(); // list of tags that was selected on saved post

        foreach (var item in editBlogModel.SelectedTags)
        {
            var selectedTag = Guid.Parse(item);
            var tagFromDatabase = await _context.Tags.FirstOrDefaultAsync(x => x.Id == selectedTag);

            tags.Add(tagFromDatabase); // adding the selected tags on the list of tags
        }

        if (oldBlogPost != null)
        {
            var newBlogPost = _mapper.Map<BlogPost>(editBlogModel); // using automapper to map the post view model to a new blog post

            // **** mapping manually the post to up to date information ****
            oldBlogPost.Heading = newBlogPost.Heading;
            oldBlogPost.Title = newBlogPost.Title;
            oldBlogPost.Content = newBlogPost.Content;
            oldBlogPost.Description = newBlogPost.Description;
            oldBlogPost.ImageUrl = newBlogPost.ImageUrl;
            oldBlogPost.PublishDate = newBlogPost.PublishDate;
            oldBlogPost.Author = newBlogPost.Author;
            oldBlogPost.Visible = newBlogPost.Visible;
            oldBlogPost.BlogTags = tags;

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = editBlogModel.Id });
        }

        return RedirectToAction("Edit", new { id = editBlogModel.Id });
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var postThatIsLookingFor = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id); // search the post that user is looking for

        // ***** delete post proccess *****
        if (postThatIsLookingFor != null)
        {
            _context.Posts.Remove(postThatIsLookingFor);

            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }

        return RedirectToAction("List");
    }
}
