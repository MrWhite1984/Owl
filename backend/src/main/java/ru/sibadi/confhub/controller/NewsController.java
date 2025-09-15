package ru.sibadi.confhub.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import ru.sibadi.confhub.dto.NewsPageRequest;
import ru.sibadi.confhub.dto.NewsRequest;
import ru.sibadi.confhub.dto.NewsResponse;
import ru.sibadi.confhub.entity.News;
import ru.sibadi.confhub.service.NewsService;
import ru.sibadi.confhub.service.PeopleServices;
import ru.sibadi.confhub.service.RedisSessionService;

import java.util.*;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/news/")
public class NewsController {

    @Autowired
    private NewsService newsService;

    @Autowired
    private PeopleServices peopleServices;

    @Autowired
    private RedisSessionService redisSessionService;

    @PostMapping("/create")
    public ResponseEntity<?> createNews(@RequestBody NewsRequest request){
        News news = new News(
                request.getTitle(),
                request.getData(),
                peopleServices.getPeopleById(redisSessionService.getUserIdBySessionToken(request.getToken())),
                request.getDateTime()
        );
        News saveNews = newsService.createNews(news);
        NewsResponse response = new NewsResponse(saveNews.getTitle(), saveNews.getData(), saveNews.getAuthor(), saveNews.getDateTime());
        return ResponseEntity.ok(response);
    }

    @PostMapping("/getPage")
    public ResponseEntity<?> getNewsPage(@RequestBody NewsPageRequest request){
        Set<News> news = newsService.getNews(request.getPageNumber(), request.getPageSize());
        List<News> list = new ArrayList<>(news);
        Collections.reverse(list);
        news = new LinkedHashSet<>(list);
        Set<NewsResponse> response = new LinkedHashSet<>();
        for(News element: news){
            NewsResponse item = new NewsResponse(
                    element.getTitle(),
                    element.getData(),
                    element.getAuthor(),
                    element.getDateTime());
            response.add(item);
        }
        return ResponseEntity.ok(response);
    }

}
