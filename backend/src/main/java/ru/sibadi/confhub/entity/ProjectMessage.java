package ru.sibadi.confhub.entity;

import jakarta.persistence.*;

import java.sql.Timestamp;
import java.util.UUID;

@Entity
@Table(name="project_messages")
public class ProjectMessage {

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

    @Column(updatable = true, nullable = false)
    private String message;

    @Column(name="date_time", updatable = false, nullable = false)
    private Timestamp dateTime;

    public ProjectMessage(People people, Projects project, String message, Timestamp dateTime){
        this.people = people;
        this.project = project;
        this.message = message;
        this.dateTime = dateTime;
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

    public String getMessage() {
        return message;
    }

    public Timestamp getDateTime() {
        return dateTime;
    }
}
