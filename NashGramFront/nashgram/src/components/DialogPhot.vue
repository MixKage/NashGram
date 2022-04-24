<template>
  <div class="text-center">
    <v-dialog
        v-model="dialog"
        width="500"
    >
      <template v-slot:activator="{  }">
        <v-btn
            color="red lighten-2"
            dark
            v-bind="is_open"
            v-on="on"
        >
          Click Me
        </v-btn>
      </template>

      <v-card>
        <v-img height="300px" v-bind:src="photo.uri"></v-img>
        <v-card-title>
          {{ photo.tag }}
        </v-card-title>

        <v-card-text>
          {{ photo.description }}
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
              color="primary"
              text
              @click="dialog = false"
          >
            Ок
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  data: () => ({
    is_open:false,

  }),
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

</style>
