package ru.sibadi.confhub.dto;

import java.sql.Date;
import java.sql.Timestamp;
import java.time.Instant;

public class NewsRequest {
    private String title;
    private String data;
    private String token; //надо токен
    private Timestamp dateTime;

    public NewsRequest(String title, String data, String token) {
        this.title = title;
        this.data = data;
        this.token = token;
        this.dateTime = Timestamp.from(Instant.now());
    }

    public String getTitle() {
        return title;
    }

    public String getData() {
        return data;
    }

    public String getToken() {
        return token;
    }

    public Timestamp getDateTime() {
        return dateTime;
    }
}
