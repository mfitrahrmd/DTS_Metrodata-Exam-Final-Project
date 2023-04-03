CREATE DATABASE openmusic;

USE openmusic;

create table dbo.albums
(
    id        int identity not null,
    name      varchar(50)  not null,
    year      smallint     not null,
    cover_url varchar(max),
    is_single bit,
    primary key (id)
);

create unique index albums_pkey
    on dbo.albums (id);

create table dbo.songs
(
    id          int identity not null,
    title       varchar(50)  not null,
    year        smallint     not null,
    genre       varchar(25)  not null,
    performer   varchar(50)  not null,
    duration    smallint,
    is_explicit bit,
    album_id    int,
    primary key (id),
    foreign key (album_id) references dbo.albums (id)
);

create unique index songs_pkey
    on dbo.songs (id);

create table dbo.users
(
    id        int identity not null,
    username  varchar(25)  not null,
    password  varchar(max) not null,
    fullname  varchar(50)  not null,
    is_active bit,
    primary key (id),
    unique (username)
);

create unique index users_pkey
    on dbo.users (id);

create unique index users_username_key
    on dbo.users (username);

create table dbo.authentications
(
    token varchar(max) not null
);

create table dbo.playlists
(
    id    int identity not null,
    name  varchar(50)  not null,
    owner int          not null,
    primary key (id),
    foreign key (owner) references dbo.users (id)

);

create unique index playlists_pkey
    on dbo.playlists (id);

create table dbo.playlists_songs
(
    id          int identity not null,
    playlist_id int          not null,
    song_id     int          not null,
    primary key (id),
    foreign key (playlist_id) references dbo.playlists (id),
    foreign key (song_id) references dbo.songs (id)
);

create unique index playlists_songs_pkey
    on dbo.playlists_songs (id);

create table dbo.collaborations
(
    id          int identity not null,
    playlist_id int          not null,
    user_id     int          not null,
    primary key (id),
    foreign key (playlist_id) references dbo.playlists (id),
    foreign key (user_id) references dbo.users (id)
);

create unique index collaborations_pkey
    on dbo.collaborations (id);

create table dbo.user_album_likes
(
    id       int identity not null,
    user_id  int          not null,
    album_id int          not null,
    primary key (id),
    foreign key (user_id) references dbo.users (id),
    foreign key (album_id) references dbo.albums (id)
);

create unique index user_album_likes_pkey
    on dbo.user_album_likes (id);

create unique index "user liked album"
    on dbo.user_album_likes (user_id, album_id);
