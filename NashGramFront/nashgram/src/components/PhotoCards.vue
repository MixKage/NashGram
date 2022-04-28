<template>
  <div id="cards" class="cards">
    <v-row>
      <v-col v-for="photo in photos" :key="photo.id" cols="12" sm="4">
        <CardsItem v-bind:photo="photo" />
      </v-col>
    </v-row>
  </div>
</template>

<script>
import CardsItem from "./PhotoCard.vue";
export default {
  components: {
    CardsItem,
  },
  props: {
    photos: {
      required: true,
    },
  },
};
</script>

<style scoped>
.cards {
  max-width: 80%;
  margin: 20px auto;
}
</style>
