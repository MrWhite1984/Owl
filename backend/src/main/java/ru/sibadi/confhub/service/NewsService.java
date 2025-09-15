package ru.sibadi.confhub.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Service;
import ru.sibadi.confhub.entity.News;
import ru.sibadi.confhub.repository.NewsRepository;

import java.sql.Timestamp;
import java.util.LinkedHashSet;
import java.util.Set;

@Service
public class NewsService {

    @Autowired
    private NewsRepository newsRepository;

    public News createNews(News news){
        News saveNews = newsRepository.save(news);
        return saveNews;
    }

    public Set<News> getNews(int pageNumber, int pageSize){
        Page<News> page = newsRepository.findAll(PageRequest.of(pageNumber, pageSize));
        return new LinkedHashSet<>(page.getContent());
    }
}
