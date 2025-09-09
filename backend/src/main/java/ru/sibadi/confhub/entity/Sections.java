package ru.sibadi.confhub.entity;

import jakarta.persistence.*;

import java.util.UUID;

@Entity
@Table (name="sections")
public class Sections {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column (updatable = false, nullable = false)
    private UUID id;

    @Column (updatable = true, nullable = false)
    private String title;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="conference", nullable = false)
    private Conferences conference;

    public Sections(String title, Conferences conference){
        this.title = title;
        this.conference = conference;
    }

    public Sections() {
    }

    public UUID getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public Conferences getConference() {
        return conference;
    }
}
