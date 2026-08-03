create table if not exists public.leaderboard_profiles (
    id uuid primary key references auth.users(id) on delete cascade,
    display_name text not null default 'CaveCode Learner',
    emblem text not null default 'crystal',
    title text not null default 'Cave Explorer',
    total_xp bigint not null default 0,
    csharp_xp bigint not null default 0,
    python_xp bigint not null default 0,
    total_lines bigint not null default 0,
    csharp_lines bigint not null default 0,
    python_lines bigint not null default 0,
    is_public boolean not null default false,
    updated_at timestamptz not null default now()
);

alter table public.leaderboard_profiles enable row level security;

drop policy if exists "Public leaderboard profiles are readable"
on public.leaderboard_profiles;

create policy "Public leaderboard profiles are readable"
on public.leaderboard_profiles
for select
using (is_public = true or auth.uid() = id);

drop policy if exists "Players can create their leaderboard profile"
on public.leaderboard_profiles;

create policy "Players can create their leaderboard profile"
on public.leaderboard_profiles
for insert
with check (auth.uid() = id);

drop policy if exists "Players can update their leaderboard profile"
on public.leaderboard_profiles;

create policy "Players can update their leaderboard profile"
on public.leaderboard_profiles
for update
using (auth.uid() = id)
with check (auth.uid() = id);

create index if not exists leaderboard_total_xp_idx
on public.leaderboard_profiles (total_xp desc);

create index if not exists leaderboard_csharp_xp_idx
on public.leaderboard_profiles (csharp_xp desc);

create index if not exists leaderboard_python_xp_idx
on public.leaderboard_profiles (python_xp desc);
