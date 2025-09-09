package ru.sibadi.confhub.entity;

import jakarta.persistence.*;

import java.sql.Timestamp;
import java.util.UUID;

@Entity
@Table (name="news")
public class News {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(updatable = false, nullable = false)
    private UUID id;

    @Column(updatable = true, nullable = false)
    private String title;

    @Column(updatable = true, nullable = false)
    private String data;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="author_id", nullable = false)
    private People author;

    @Column(name="date_time", updatable = false, nullable = false)
    private Timestamp dateTime;

    public News(String title, String data, People author, Timestamp dateTime){
        this.title = title;
        this.data = data;
        this.author = author;
        this.dateTime = dateTime;
    }

    public News() {
    }

    public UUID getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public String getData() {
        return data;
    }

    public People getAuthor() {
        return author;
    }

    public Timestamp getDateTime() {
        return dateTime;
    }
}
