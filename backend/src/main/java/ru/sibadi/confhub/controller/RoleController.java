package ru.sibadi.confhub.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import ru.sibadi.confhub.dto.RoleRequest;
import ru.sibadi.confhub.entity.Roles;
import ru.sibadi.confhub.service.RoleServices;

//@RestController
//@RequestMapping("/api/roles/")
public class RoleController {

    @Autowired
    private RoleServices roleServices;

    @PostMapping
    public ResponseEntity<Roles> createRole(@RequestBody RoleRequest request){
        Roles role = new Roles(request.getTitle());
        Roles saved = roleServices.createRole(role);
        return ResponseEntity.ok(saved);
    }
}
