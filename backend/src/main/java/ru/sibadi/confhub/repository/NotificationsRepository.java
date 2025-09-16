package ru.sibadi.confhub.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.sibadi.confhub.entity.Notifications;

import java.util.UUID;

public interface NotificationsRepository extends JpaRepository<Notifications, UUID> {
}
