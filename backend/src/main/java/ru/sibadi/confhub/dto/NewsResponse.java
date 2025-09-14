package ru.sibadi.confhub.dto;

import ru.sibadi.confhub.entity.People;

import java.sql.Timestamp;

public class NewsResponse {
    private String title;
    private String data;
    private PeopleLightResponse author;
    private Timestamp dateTime;

    public NewsResponse(String title, String data, People author, Timestamp dateTime) {
        this.title = title;
        this.data = data;
        this.author = new PeopleLightResponse(
                author.getSurname(),
                author.getName(),
                author.isVerified(),
                author.getProfilePhoto(),
                author.getJobTitle());
        this.dateTime = dateTime;
    }

    public String getTitle() {
        return title;
    }

    public String getData() {
        return data;
    }

    public PeopleLightResponse getAuthor() {
        return author;
    }

    public Timestamp getDateTime() {
        return dateTime;
    }
}
