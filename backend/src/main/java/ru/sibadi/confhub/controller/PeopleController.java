package ru.sibadi.confhub.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.sibadi.confhub.dto.PeopleRequest;
import ru.sibadi.confhub.entity.People;
import ru.sibadi.confhub.service.PeopleServices;
import ru.sibadi.confhub.service.RoleServices;

@RestController
@RequestMapping("/api/people/")
public class PeopleController {

    @Autowired
    private PeopleServices peopleServices;

    @Autowired
    private RoleServices roleServices;

    @PostMapping
    public ResponseEntity<People> createPeople(@RequestBody PeopleRequest request){
        People people = new People(
                request.getSurname(),
                request.getName(),
                request.getPatronymic(),
                request.getEducationalInstitution(),
                request.getJobTitle(),
                request.getCity(),
                request.getPhone(),
                request.getEmail(),
                request.getPassword(),
                roleServices.getRolesByTitles(request.getRoles())
        );
        People savedPeople = peopleServices.createPeople(people);
        return ResponseEntity.ok(savedPeople);
    }
}
