<template>
  <div id="card">
    <v-card>
      <v-img
        height="200px"
        :aspect-ratio="16 / 9"
        @click="handlePhotoDialog"
        class="card__image"
        v-bind:src="`${photo.image}`"
      ></v-img>
      <div class="card__container">
        <div id="card__sec_container">
          <v-card-title>#{{ photo.tag }}</v-card-title>

          <div class="card__like_container">
            <v-btn class="card__like" icon small @click="handleLike"
              ><v-icon v-if="isLiked.liked"> mdi-heart </v-icon>
              <v-icon v-else> mdi-heart-outline </v-icon>
            </v-btn>
            <v-card-text class="card__like_text">{{
              this.likeCount
            }}</v-card-text>
          </div>
        </div>

        <v-dialog v-model="dialog" width="500">
          <template v-slot:activator="{ on, attrs }">
            <v-btn class="photo__info" v-bind="attrs" v-on="on">
              Подробнее
            </v-btn>
          </template>
          <v-card>
            <v-img
              max-height="300px"
              :aspect-ratio="16 / 9"
              contain
              @click="handlePhotoDialog"
              class="card__image"
              v-bind:src="`${photo.image}`"
            ></v-img>
            <div class="card__sec_container">
              <v-card-title>#{{ photo.tag }}</v-card-title>

              <div
                v-if="this.isAuth"
                class="card__like_container card__like_seccontainer"
              >
                <v-btn
                  class="card__like card__seclike"
                  icon
                  small
                  @click="handleLike"
                  ><v-icon v-if="isLiked.liked"> mdi-heart </v-icon>
                  <v-icon v-else> mdi-heart-outline </v-icon>
                </v-btn>
                <v-card-text class="card__like_sectext">{{
                  this.likeCount
                }}</v-card-text>
              </div>
            </div>
            <v-card-subtitle>Автор:{{ this.authorInfo.name }}</v-card-subtitle>
            <v-card-text>
              {{ this.description }}
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions v-if="this.$store.getters.GET_AUTH">
              <v-btn
                v-if="this.isAuthor"
                color="error"
                @click="handleDelete"
                class="mr-4"
                icon
              >
                <v-icon>mdi-delete</v-icon>
              </v-btn>
              <v-btn
                v-if="this.isAuthor"
                color="black"
                @click="handleChangeDialog"
                class="mr-4"
                icon
              >
                <v-icon>mdi-pencil</v-icon>
              </v-btn>

              <v-btn
                v-else
                color="error"
                @click="handleReportDialog"
                class="mr-4"
                icon
              >
                <v-icon>mdi-alert</v-icon>
              </v-btn>
              <v-btn @click="handleAuthorDialog">Об авторе</v-btn>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="dialog = false"> Ок </v-btn>
            </v-card-actions>
          </v-card>
          <v-dialog v-model="reportDialog" persistent max-width="60%">
            <v-card>
              <v-card-title>Репорт на пост</v-card-title>
              <v-form class="changeform" ref="form" lazy-validation>
                <v-text-field
                  v-model="report"
                  label="Описание репорта"
                ></v-text-field>
              </v-form>
              <v-card-actions>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="red darken-1" text class="mr-4" @click="reset">
                    Сбросить заполнение
                  </v-btn>
                  <v-btn color="green darken-1" text @click="handleReport">
                    Зарепортить
                  </v-btn>
                </v-card-actions>
              </v-card-actions>
              <v-btn
                color="black darken-1"
                class="changedialog__dialog-close"
                icon
                text
                @click="handleReportDialog"
                ><v-icon>mdi-close</v-icon></v-btn
              >
            </v-card>
          </v-dialog>
        </v-dialog>
        <v-dialog v-model="changeDialog" persistent max-width="60%">
          <v-card>
            <v-card-title>Изменение поста</v-card-title>
            <v-form class="changeform" ref="form" lazy-validation>
              <v-text-field
                v-model="newdescription"
                label="Описание"
              ></v-text-field>
            </v-form>
            <v-card-actions>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="red darken-1" text class="mr-4" @click="reset">
                  Сбросить заполнение
                </v-btn>
                <v-btn color="green darken-1" text @click="handleChange">
                  Запостить
                </v-btn>
              </v-card-actions>
            </v-card-actions>
            <v-btn
              color="black darken-1"
              class="changedialog__dialog-close"
              icon
              text
              @click="handleChangeDialog"
              ><v-icon>mdi-close</v-icon></v-btn
            >
          </v-card>
        </v-dialog>
        <v-dialog v-model="authorDialog" persistent max-width="20%">
          <v-card>
            <v-card-title>Имя:{{ this.authorInfo.name }}</v-card-title>
            <v-spacer></v-spacer>
            <v-card-subtitle>Почта:{{ this.authorInfo.email }}</v-card-subtitle>
            <v-card-text>Статус:{{ this.authorInfo.status }}</v-card-text>
            <v-btn
              color="black darken-1"
              class="author__dialog-close"
              icon
              text
              @click="handleAuthorDialog"
              ><v-icon>mdi-close</v-icon></v-btn
            >
          </v-card>
        </v-dialog>
        <v-dialog v-model="logDialog" persistent max-width="290">
          <v-card>
            <v-card-title class="text-h5"> Вы не вошли. </v-card-title>
            <v-card-text
              >Пожалуйтса войдите или если впервые тут, то зарегистрируйтесь и
              войдите.</v-card-text
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="green darken-1" text @click="this.handleNotLog">
                Войти
              </v-btn>
              <v-btn color="green darken-1" text @click="closeNotLogDialog">
                Ок
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog
          v-model="photoDialog"
          persistent
          max-width="50%"
          :width="this.imageProp.width"
        >
          <v-img
            contain
            :width="this.imageProp.width"
            max-heighth="0.5vh"
            v-bind:src="`${photo.image}`"
            ><v-btn
              class="card__photo-close_button"
              dark
              icon
              text
              @click="handlePhotoDialog"
              ><v-icon>mdi-close</v-icon></v-btn
            ></v-img
          >
        </v-dialog>
      </div>
    </v-card>
  </div>
</template>

<script>
import { HTTP } from "../api/API";

export default {
  data() {
    return {
      reportDialog: false,
      authorDialog: false,
      changeDialog: false,
      photoDialog: false,
      dialog: false,
      logDialog: false,
      author: "",
      isAuth: false,
      likes: [],
      likeCount: 0,
      isLiked: { liked: false },
      isAuthor: false,
      likeId: 0,
      report: "",
      description: "",
      newdescription: "",
      authorInfo: {
        name: "",
        email: "",
        status: "",
      },
      imageProp: { height: 0, width: 0 },
    };
  },
  props: {
    photo: {
      type: Object,
      required: true,
    },
    getposts: {
      required: true,
    },
  },
  computed: {},
  methods: {
    handleReportDialog() {
      this.reportDialog = !this.reportDialog;
    },
    handleAuthorDialog() {
      this.authorDialog = !this.authorDialog;
    },
    handlePhotoDialog() {
      this.photoDialog = !this.photoDialog;
    },
    closeNotLogDialog() {
      this.logDialog = false;
    },
    handleNotLog() {
      this.logDialog = false;
      this.$router.push("/login");
    },
    notLogin() {
      this.logDialog = true;
    },
    getAuthorInfo() {
      const token = localStorage.getItem("token");
      console.log(this.photo.author);
      HTTP.get("GetNameFromId", {
        params: { input: this.photo.author },
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.authorInfo.name = res.data;
        })
        .catch((err) => {
          console.log(err.status);
        });
      HTTP.get("GetEmailFromId", {
        params: { input: this.photo.author },
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.authorInfo.email = res.data;
        })
        .catch((err) => {
          console.log(err.status);
        });
      HTTP.get("GetStatusFromId", {
        params: { input: this.photo.author },
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.authorInfo.status = res.data;
        })
        .catch((err) => {
          console.log(err.status);
        });
    },
    putlike() {
      if (this.isAuth) {
        const token = localStorage.getItem("token");
        HTTP.post(
          "CreateLike",
          {
            id_post: this.photo.id,
            id_account: this.$store.getters.GET_CURRUSER.id,
          },
          {
            headers: {
              Authorization: `Basic ${token}`,
            },
          }
        )
          .then(() => {
            this.updlikes();
          })
          .catch((err) => {
            console.log(err);
          });
      } else {
        this.notLogin();
      }
    },
    reset() {
      this.$refs.form.reset();
    },
    unputlike() {
      const token = localStorage.getItem("token");
      HTTP.delete("DeleteLikeFromId", {
        headers: {
          Authorization: `Basic ${token}`,
        },
        params: {
          id: this.likeId,
        },
      })
        .then(() => {
          this.updlikes();
        })
        .catch((err) => {
          console.log(err);
        });
    },
    async handleLike() {
      if (this.isLiked.liked) {
        this.unputlike();
      } else {
        this.putlike();
      }
    },
    getDesc() {
      const token = localStorage.getItem("token");
      HTTP.get("GetDescryptionFromId", {
        params: { input: this.photo.id },
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.photo.descryption = res.data;
          this.checkDesc();
        })
        .catch((err) => {
          console.log(err.status);
        });
    },
    handleReport() {
      const token = localStorage.getItem("token");
      console.log(token);
      HTTP.post(
        "CreateReport",
        {
          id: `${this.photo.id}`,
          text: `${this.report}`,
        },
        {
          headers: {
            Authorization: `Basic ${token}`,
          },
        }
      )
        .then(() => {
          this.handleReportDialog();
        })
        .catch(() => {
          this.$store.dispatch("SET_ERRDIALOG", true);
        });
    },
    handleChange() {
      const token = localStorage.getItem("token");
      console.log(token);
      HTTP.post(
        "UpdateDescryptionFromIdPost",
        {
          id: `${this.photo.id}`,
          text: `${this.newdescription}`,
        },
        {
          headers: {
            Authorization: `Basic ${token}`,
          },
        }
      )
        .then(() => {
          this.getDesc();
          this.handleChangeDialog();
          this.reset();
        })
        .catch(() => {
          this.$store.dispatch("SET_ERRDIALOG", true);
        });
    },

    handleDelete() {
      const token = localStorage.getItem("token");
      HTTP.delete("DeletePostsFromIdPost", {
        headers: {
          Authorization: `Basic ${token}`,
        },
        params: {
          id: this.photo.id,
        },
      })
        .then(() => {
          this.getposts();
          this.dialog = false;
        })
        .catch((err) => {
          console.log(err);
        });
    },
    async updlikes() {
      const token = localStorage.getItem("token");
      HTTP.get("GetLikesFromIdPost", {
        params: { id: this.photo.id },
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.likes = res.data;
          this.likeCount = this.likes.length;
          if (this.likeCount > 0) {
            this.likes.forEach((e) => {
              if (e.idAccount === this.$store.getters.GET_CURRUSER.id) {
                this.isLiked.liked =
                  e.idAccount === this.$store.getters.GET_CURRUSER.id;
                this.likeId = e.id;
              } else {
                this.isLiked.liked = false;
              }
            });
          } else {
            this.isLiked.liked = false;
          }
        })
        .catch((err) => {
          console.log(err.status);
        });
    },
    checkDesc() {
      if (this.photo.descryption === "") {
        this.description = "";
      } else {
        this.description = `Описание:${this.photo.descryption}`;
      }
    },
    handleChangeDialog() {
      this.changeDialog = !this.changeDialog;
    },
    handleImage() {
      const i = new Image();
      i.onload = () => {
        this.imageProp.width = i.width;
        this.imageProp.height = i.height;
      };
      i.src = this.photo.image;
    },
  },
  async created() {
    this.getAuthorInfo();
    this.handleImage();
    this.checkDesc();
    this.isAuth = this.$store.getters.GET_AUTH;
    if (this.photo.author === this.$store.getters.GET_CURRUSER.id) {
      this.isAuthor = true;
    }
    await this.updlikes();
  },
};
</script>
<style scoped>
>>> .v-dialog {
  overflow-y: visible;
}
>>> .v-responsive {
  overflow: visible;
}
#card__sec_container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
.changeform {
  padding: 50px;
}
.author__dialog-close {
  position: absolute;
  top: 10px;
  right: 10px;
}
.changedialog__dialog-close {
  position: absolute;
  top: 10px;
  right: 10px;
}
.card__photo-close_button {
  position: relative;
  top: -30px;
  left: 100%;
}
.card__image:hover {
  cursor: pointer;
}
.card__container {
  display: flex;
  flex-direction: column;
}
.card__sec_container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}
.card__like {
  margin: 20px 20px 0px 20px;
}
.card__like_container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  text-align: center;
}
.card__like_text {
  padding-top: 0;
}
.card__like_seccontainer {
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.card__like_sectext {
  margin: auto 10px;
  padding: 0;
}
.card__seclike {
  margin: auto;
}
</style>
