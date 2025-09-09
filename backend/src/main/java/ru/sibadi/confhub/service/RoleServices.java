package ru.sibadi.confhub.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import ru.sibadi.confhub.entity.Roles;
import ru.sibadi.confhub.repository.PeopleRepository;
import ru.sibadi.confhub.repository.RolesRepository;

@Service
public class RoleServices {

    @Autowired
    private RolesRepository rolesRepository;

    @Autowired
    private PeopleRepository peopleRepository;

    public Roles createRole(Roles role){
        Roles savedRole = rolesRepository.save(role);
        return savedRole;
    }

}
