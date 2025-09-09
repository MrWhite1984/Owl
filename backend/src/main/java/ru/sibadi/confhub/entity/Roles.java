package ru.sibadi.confhub.entity;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;
import java.util.UUID;

@Entity
@Table(name="roles")
public class Roles {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(updatable = false, nullable = false)
    private UUID id;

    @Column(updatable = true, nullable = false)
    private String title;

    @ManyToMany(mappedBy = "roles", fetch = FetchType.LAZY)
    private Set<People> people = new HashSet<>();

    public Roles(String title){
        this.title = title;
    }

    public Roles() {
    }

    public UUID getId() {
        return id;
    }

    public String getTitle() {
        return title;
    }
}