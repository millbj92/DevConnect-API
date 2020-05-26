using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevConnectAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    location_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    StreetTwo = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.location_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    Parentuser_id = table.Column<int>(nullable: true),
                    userGuid = table.Column<Guid>(nullable: false),
                    isConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Users_Users_Parentuser_id",
                        column: x => x.Parentuser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlockList",
                columns: table => new
                {
                    relation_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    BlockId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockList", x => x.relation_id);
                    table.ForeignKey(
                        name: "FK_BlockList_Users_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_BlockList_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    relation_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    RequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => x.relation_id);
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_FriendRequests_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    relation_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    FriendId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.relation_id);
                    table.ForeignKey(
                        name: "FK_Friends_Users_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Friends_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    message_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Read = table.Column<DateTime>(nullable: true),
                    senderuser_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.message_id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_senderuser_id",
                        column: x => x.senderuser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoAlbums",
                columns: table => new
                {
                    album_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    owneruser_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoAlbums", x => x.album_id);
                    table.ForeignKey(
                        name: "FK_PhotoAlbums_Users_owneruser_id",
                        column: x => x.owneruser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    post_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Owneruser_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_Posts_Users_Owneruser_id",
                        column: x => x.Owneruser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserStatuses",
                columns: table => new
                {
                    status_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatuses", x => x.status_id);
                    table.ForeignKey(
                        name: "FK_UserStatuses_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    user_message_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: true),
                    message_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.user_message_id);
                    table.ForeignKey(
                        name: "FK_UserMessages_Messages_message_id",
                        column: x => x.message_id,
                        principalTable: "Messages",
                        principalColumn: "message_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMessages_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    photo_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    album_id = table.Column<int>(nullable: false),
                    caption = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    img_url = table.Column<string>(nullable: true),
                    owneruser_id = table.Column<int>(nullable: true),
                    post_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.photo_id);
                    table.ForeignKey(
                        name: "FK_Photos_PhotoAlbums_album_id",
                        column: x => x.album_id,
                        principalTable: "PhotoAlbums",
                        principalColumn: "album_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photos_Users_owneruser_id",
                        column: x => x.owneruser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    comment_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(nullable: true),
                    post_id = table.Column<int>(nullable: true),
                    Owneruser_id = table.Column<int>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_PostComments_Users_Owneruser_id",
                        column: x => x.Owneruser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    profile_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: false),
                    Bio = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    UserStatusstatus_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.profile_id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserStatuses_UserStatusstatus_id",
                        column: x => x.UserStatusstatus_id,
                        principalTable: "UserStatuses",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLikes",
                columns: table => new
                {
                    like_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(nullable: true),
                    PostCommentcomment_id = table.Column<int>(nullable: true),
                    post_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => x.like_id);
                    table.ForeignKey(
                        name: "FK_UserLikes_PostComments_PostCommentcomment_id",
                        column: x => x.PostCommentcomment_id,
                        principalTable: "PostComments",
                        principalColumn: "comment_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLikes_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "post_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLikes_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlaces",
                columns: table => new
                {
                    workplace_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkLocationlocation_id = table.Column<int>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    UserProfileprofile_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlaces", x => x.workplace_id);
                    table.ForeignKey(
                        name: "FK_WorkPlaces_UserProfiles_UserProfileprofile_id",
                        column: x => x.UserProfileprofile_id,
                        principalTable: "UserProfiles",
                        principalColumn: "profile_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlaces_Locations_WorkLocationlocation_id",
                        column: x => x.WorkLocationlocation_id,
                        principalTable: "Locations",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockList_BlockId",
                table: "BlockList",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockList_user_id",
                table: "BlockList",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_RequestId",
                table: "FriendRequests",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_user_id",
                table: "FriendRequests",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendId",
                table: "Friends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_user_id",
                table: "Friends",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_senderuser_id",
                table: "Messages",
                column: "senderuser_id");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoAlbums_owneruser_id",
                table: "PhotoAlbums",
                column: "owneruser_id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_album_id",
                table: "Photos",
                column: "album_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_owneruser_id",
                table: "Photos",
                column: "owneruser_id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_post_id",
                table: "Photos",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_Owneruser_id",
                table: "PostComments",
                column: "Owneruser_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_post_id",
                table: "PostComments",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Owneruser_id",
                table: "Posts",
                column: "Owneruser_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_PostCommentcomment_id",
                table: "UserLikes",
                column: "PostCommentcomment_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_post_id",
                table: "UserLikes",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_user_id",
                table: "UserLikes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_message_id",
                table: "UserMessages",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_user_id",
                table: "UserMessages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserStatusstatus_id",
                table: "UserProfiles",
                column: "UserStatusstatus_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_user_id",
                table: "UserProfiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Parentuser_id",
                table: "Users",
                column: "Parentuser_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatuses_user_id",
                table: "UserStatuses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlaces_UserProfileprofile_id",
                table: "WorkPlaces",
                column: "UserProfileprofile_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlaces_WorkLocationlocation_id",
                table: "WorkPlaces",
                column: "WorkLocationlocation_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockList");

            migrationBuilder.DropTable(
                name: "FriendRequests");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "UserLikes");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "WorkPlaces");

            migrationBuilder.DropTable(
                name: "PhotoAlbums");

            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "UserStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
