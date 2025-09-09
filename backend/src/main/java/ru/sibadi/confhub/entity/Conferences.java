package ru.sibadi.confhub.entity;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

@Entity
@Table (name="conferences")
public class Conferences {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column (updatable = false, nullable = false)
    private UUID id;

    @Column (updatable = true, nullable = false)
    private String title;

    @Column (updatable = true, nullable = false)
    private String date;

    @Column (name="is_active", updatable = true, nullable = false)
    private boolean isActive;

    @Column (name="program_file", updatable = true, nullable = true)
    private String programFile;

    @Column (name="full_text", updatable = true, nullable = true)
    private String fullText;

    @OneToMany (mappedBy = "conference", fetch = FetchType.LAZY)
    private Set<Sections> sections = new HashSet<>();

    public Conferences(String title, String date, boolean isActive, String programFile, String fullText) {
        this.title = title;
        this.date = date;
        this.isActive = isActive;
        this.programFile = programFile;
        this.fullText = fullText;
    }

    public UUID getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public String getDate() {
        return date;
    }

    public boolean isActive() {
        return isActive;
    }

    public String getProgramFile() {
        return programFile;
    }

    public String getFullText() {
        return fullText;
    }

}
