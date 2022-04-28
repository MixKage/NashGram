<template>
  <div id="card">
    <v-card>
      <v-img height="200px" v-bind:src="photo.uri"></v-img>
      <div class="card__container">
        <div id="card__sec_container">
          <v-card-title>#{{ photo.tag }}</v-card-title>

          <div class="card__like_container">
            <v-btn class="card__like" icon small
              ><v-icon> mdi-heart </v-icon>
            </v-btn>
            <v-card-text class="card__like_text">{{ photo.likes }}</v-card-text>
          </div>
        </div>

        <v-dialog v-model="dialog" width="500">
          <template v-slot:activator="{ on, attrs }">
            <v-btn class="photo__info" v-bind="attrs" v-on="on">
              Подробнее
            </v-btn>
          </template>
          <v-card>
            <v-img height="300px" v-bind:src="photo.uri"></v-img>
            <div class="card__sec_container">
              <v-card-title>#{{ photo.tag }}</v-card-title>

              <div class="card__like_container card__like_seccontainer">
                <v-btn class="card__like card__seclike" icon small
                ><v-icon> mdi-heart </v-icon>
                </v-btn>
                <v-card-text class="card__like_sectext">{{ photo.likes }}</v-card-text>
              </div>
            </div>
            <v-card-subtitle>Автор:{{photo.author}}</v-card-subtitle>
            <v-card-text>
              {{ photo.description }}
            </v-card-text>

            <v-divider></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="dialog = false">
                Ок
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </div>
    </v-card>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dialog: false,
    };
  },
  props: {
    photo: {
      type: Object,
      required: true,
    },
    i: {
      required: true,
    },
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
.card__sec_container
{
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
.card__like_sectext
{
  margin: auto 10px;
  padding: 0;
}
.card__seclike{
  margin: auto;
}
</style>
