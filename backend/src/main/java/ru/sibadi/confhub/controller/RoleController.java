package ru.sibadi.confhub.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import ru.sibadi.confhub.dto.IdRequest;
import ru.sibadi.confhub.dto.RoleRequest;
import ru.sibadi.confhub.entity.Roles;
import ru.sibadi.confhub.service.RoleServices;

import javax.management.relation.Role;

@RestController
@RequestMapping("/api/roles/")
public class RoleController {

    @Autowired
    private RoleServices roleServices;

    @PostMapping("/getRole")
    public ResponseEntity<?> getRole(@RequestBody IdRequest request){
        Roles role = roleServices.getRoleById(request.getId());
        return ResponseEntity.ok(role);
    }
}
