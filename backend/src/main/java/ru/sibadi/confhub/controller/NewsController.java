package ru.sibadi.confhub.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import ru.sibadi.confhub.dto.NewsRequest;
import ru.sibadi.confhub.dto.NewsResponse;
import ru.sibadi.confhub.entity.News;
import ru.sibadi.confhub.service.NewsService;
import ru.sibadi.confhub.service.PeopleServices;

import java.util.UUID;

@RestController
@RequestMapping("/api/news/")
public class NewsController {

    @Autowired
    private NewsService newsService;

    @Autowired
    private PeopleServices peopleServices;

    @PostMapping("/create")
    public ResponseEntity<?> createNews(@RequestBody NewsRequest request){
        News news = new News(
                request.getTitle(),
                request.getData(),
                peopleServices.getPeopleById(UUID.fromString(request.getAuthorId())),
                request.getDateTime()
        );
        News saveNews = newsService.createNews(news);
        NewsResponse response = new NewsResponse(saveNews.getTitle(), saveNews.getData(), saveNews.getAuthor(), saveNews.getDateTime());
        return ResponseEntity.ok(response);
    }


}
