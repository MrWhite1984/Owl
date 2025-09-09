package ru.sibadi.confhub.entity;


import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

@Entity
@Table (name="projects")
public class Projects {

    @Id
    @GeneratedValue (strategy = GenerationType.UUID)
    @Column (updatable = false, nullable = false)
    private UUID id;

    @Column (updatable = true, nullable = false)
    private String title;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="scientific_director", nullable = false)
    private People scientificDirector;

    @Column (updatable = true, nullable = false)
    private String state;

    @Column (name="is_changeable", updatable = true, nullable = false)
    private boolean isChangeable;

    @Column (name="project_file", updatable = true, nullable = false)
    private String projectFile;

    @Column (name="review_file", updatable = true, nullable = true)
    private String reviewFile;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="section", nullable = false)
    private Sections section;

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(
            name = "project_participants",
            joinColumns = @JoinColumn(name = "project_id"),
            inverseJoinColumns = @JoinColumn(name = "people_id")
    )
    private Set<People> participants = new HashSet<>();

    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(
            name = "project_moderators",
            joinColumns = @JoinColumn(name = "project_id"),
            inverseJoinColumns = @JoinColumn(name = "people_id")
    )
    private Set<People> moderators = new HashSet<>();

    public Projects(
            String title,
            People scientificDirector,
            String state,
            Sections section){
        this.title = title;
        this.scientificDirector = scientificDirector;
        this.state = state;
        isChangeable = true;
        this.section = section;
    }

    public Projects() {
    }

    public UUID getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }

    public People getScientificDirector() {
        return scientificDirector;
    }

    public String getState() {
        return state;
    }

    public boolean isChangeable() {
        return isChangeable;
    }

    public String getProjectFile() {
        return projectFile;
    }

    public String getReviewFile() {
        return reviewFile;
    }

    public Sections getSection() {
        return section;
    }

    public Set<People> getParticipants() {
        return participants;
    }

    public Set<People> getModerators() {
        return moderators;
    }
}
