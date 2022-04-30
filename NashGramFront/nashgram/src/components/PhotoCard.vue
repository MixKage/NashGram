<template>
  <div id="card">
    <v-card>
      <v-img height="200px" v-bind:src="`${photo.image}`"></v-img>
      <div class="card__container">
        <div id="card__sec_container">
          <v-card-title>#{{ photo.tag }}</v-card-title>

          <div class="card__like_container">
            <v-btn class="card__like" icon small @click="handleLike"
              ><v-icon> mdi-heart </v-icon>
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
            <v-img height="300px" v-bind:src="`${photo.image}`"></v-img>
            <div class="card__sec_container">
              <v-card-title>#{{ photo.tag }}</v-card-title>

              <div class="card__like_container card__like_seccontainer">
                <v-btn
                  class="card__like card__seclike"
                  icon
                  small
                  @click="handleLike"
                  ><v-icon> mdi-heart </v-icon>
                </v-btn>
                <v-card-text class="card__like_sectext">{{
                  this.likeCount
                }}</v-card-text>
              </div>
            </div>
            <v-card-subtitle>Автор:{{ this.author }}</v-card-subtitle>
            <v-card-text>
              {{ photo.description }}
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="dialog = false"> Ок </v-btn>
            </v-card-actions>
          </v-card>
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
      dialog: false,
      author: "",
      likes: [],
      likeCount: 0,
      isLiked: { liked: false },
      likeId: 0,
    };
  },
  props: {
    photo: {
      type: Object,
      required: true,
    },
  },
  methods: {
    putlike() {
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
    handleLike() {
      if (this.isLiked.liked) {
        this.unputlike();
      } else {
        this.putlike();
      }
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
          this.likes.forEach((e) => {
            this.isLiked.liked =
              e.idAccount === this.$store.getters.GET_CURRUSER.id;
            this.likeCount = res.data.length;
          });
        })
        .catch((err) => {
          console.log(err.status);
        });
      HTTP.get("GetLikesFromIdPost", { params: { id: this.photo.id } })
        .then((res) => {
          res.data.forEach((e) => {
            if (e.idAccount === this.$store.getters.GET_CURRUSER.id) {
              this.likeId = e.id;
            }
          });
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
  async created() {
    await this.updlikes();
    const token = localStorage.getItem("token");
    HTTP.get("GetLogin", {
      params: { id: this.photo.author },
      headers: {
        Authorization: `Basic ${token}`,
      },
    })
      .then((res) => {
        this.author = res.data;
      })
      .catch((err) => {
        console.log(err);
      });
  },
};
</script>
<style scoped>
#card__sec_container {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
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
