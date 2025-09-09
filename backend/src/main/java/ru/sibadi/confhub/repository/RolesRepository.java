package ru.sibadi.confhub.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.sibadi.confhub.entity.Roles;

import java.util.UUID;

public interface RolesRepository extends JpaRepository<Roles, UUID> {
}
