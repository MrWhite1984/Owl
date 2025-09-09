package ru.sibadi.confhub.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.StringRedisTemplate;
import org.springframework.stereotype.Service;
import org.apache.commons.codec.digest.DigestUtils;
import ru.sibadi.confhub.config.SessionConfig;

import java.util.UUID;
import java.util.concurrent.TimeUnit;

@Service
public class RedisSessionService {

    @Autowired
    private StringRedisTemplate redisTemplate;

    private static final String SESSION_KEY_PREFIX = "sibadi:session";

    public String createSession(UUID userId) {
        String rawToken = UUID.randomUUID().toString();
        String sessionToken = DigestUtils.sha256Hex(rawToken);

        redisTemplate.opsForValue().set(
                SESSION_KEY_PREFIX + sessionToken,
                userId.toString(),
                30, // 30 минут
                TimeUnit.MINUTES
        );

        return sessionToken;
    }

    public UUID getUserIdBySessionToken(String sessionToken) {
        String userIdStr = redisTemplate.opsForValue().get(SESSION_KEY_PREFIX + sessionToken);
        if (userIdStr != null) {
            return UUID.fromString(userIdStr);
        }
        return null;
    }

    public void deleteSession(String sessionToken) {
        redisTemplate.delete(SESSION_KEY_PREFIX + sessionToken);
    }
}
