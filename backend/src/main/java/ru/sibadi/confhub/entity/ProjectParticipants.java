package ru.sibadi.confhub.entity;


import jakarta.persistence.*;

import java.util.UUID;

@Entity
@Table(name="project_participants")
public class ProjectParticipants {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(updatable = false, nullable = false)
    private UUID id;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="people_id", nullable = false)
    private People people;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "project_id", nullable = false)
    private Projects project;

    @Column(name="is_accepted", updatable = true, nullable = false)
    private boolean isAccepted;

    public ProjectParticipants(People people, Projects project, boolean isAccepted) {
        this.people = people;
        this.project = project;
        this.isAccepted = isAccepted;
    }

    public UUID getId() {
        return id;
    }

    public People getPeople() {
        return people;
    }

    public Projects getProject() {
        return project;
    }

    public boolean isAccepted() {
        return isAccepted;
    }

    public void setAccepted(boolean accepted) {
        isAccepted = accepted;
    }
}
