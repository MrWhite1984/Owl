package ru.sibadi.confhub.entity;

import jakarta.persistence.*;

import java.sql.Timestamp;
import java.util.UUID;

@Entity
@Table(name="notifications")
public class Notifications {

    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    @Column(updatable = false, nullable = false)
    private UUID id;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name="target_people_id", nullable = false)
    private People targetPeople;

    @Column(name="notify_data", updatable = false, nullable = false)
    private String data;

    @Column(name="date_time", updatable = false, nullable = false)
    private Timestamp dateTime;

    @Column(name="is_cheked", updatable = true, nullable = false)
    private boolean isCheked;

    public Notifications(People targetPeople, String data, Timestamp dateTime, boolean isCheked) {
        this.targetPeople = targetPeople;
        this.data = data;
        this.dateTime = dateTime;
        this.isCheked = isCheked;
    }

    public UUID getId() {
        return id;
    }

    public People getTargetPeople() {
        return targetPeople;
    }

    public String getData() {
        return data;
    }

    public Timestamp getDateTime() {
        return dateTime;
    }

    public boolean isCheked() {
        return isCheked;
    }

    public void setCheked(boolean cheked) {
        isCheked = cheked;
    }
}
