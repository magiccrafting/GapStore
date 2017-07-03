﻿
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gap.Articles.Services;
using Gap.Entities.Articles;
using Gap.SuperZapatos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gap.SuperZapatos.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        
        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]ArticleModel model)
        {
            var article = _mapper.Map<ArticleModel, Article>(model);
            await _articleService.Insert(article);
            return Ok(new
            {
                Success = true
            });
        }

        [HttpGet("services/articles")]
        public async Task<ActionResult> Get()
        {
            var result = await _articleService.GetAllWithStore();
            var articles = _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleModel>>(result);
            
            return Ok(new
            {
                Success = true,
                TotalElements = result.Count(),
                Articles = articles
            });
        }
        
        
        [HttpGet("services/articles/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _articleService.GetWihtStore(id);
            var article = _mapper.Map<Article, ArticleModel>(result);
            
            return Ok(new
            {
                Success = true,
                Article = article
            });
        }
        
        [HttpGet("services/articles/stores/{id}")]
        public async Task<ActionResult> Stores(int id)
        {
            var result = await _articleService.GetAllByStoreId(id);
            var articles = _mapper.Map<IEnumerable<Article>, IEnumerable<ArticleModel>>(result);
            
            return Ok(new
            {
                Success = true,
                TotalElements = result.Count(),
                Articles = articles
            });
        }
    }
}