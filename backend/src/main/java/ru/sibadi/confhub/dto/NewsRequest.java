package ru.sibadi.confhub.dto;

import java.sql.Date;
import java.sql.Timestamp;
import java.time.Instant;

public class NewsRequest {
    private String title;
    private String data;
    private String authorId;
    private Timestamp dateTime;

    public NewsRequest(String title, String data, String authorId) {
        this.title = title;
        this.data = data;
        this.authorId = authorId;
        this.dateTime = Timestamp.from(Instant.now());
    }

    public String getTitle() {
        return title;
    }

    public String getData() {
        return data;
    }

    public String getAuthorId() {
        return authorId;
    }

    public Timestamp getDateTime() {
        return dateTime;
    }
}
