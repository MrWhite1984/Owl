INSERT INTO roles (id, title) VALUES (gen_random_uuid(), 'ADMIN') ON CONFLICT (title) DO NOTHING;
INSERT INTO roles (id, title) VALUES (gen_random_uuid(), 'USER') ON CONFLICT (title) DO NOTHING;
INSERT INTO roles (id, title) VALUES (gen_random_uuid(), 'MODER') ON CONFLICT (title) DO NOTHING;