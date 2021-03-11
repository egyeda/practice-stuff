<template>

  <div>
    <v-text-field label="Search" placeholder="Cork, flip, etc..." outlined prepend-inner-icon="mdi-magnify"
                  v-model="filter"></v-text-field>
    <div v-for="t in filteredTricks">
      {{ t.name }} - {{ t.description }}
    </div>
  </div>
</template>

<script>

export default {
  name: "trick-list",
  props: {
    tricks: {
      required: true,
      type: Array,
    }
  },
  data: () => ({
    filter: "",
  }),
  computed: {
    filteredTricks() {
      if (!this.filter) {
        return this.tricks;
      }

      const normalize = this.filter.trim().toLowerCase();

      return this.tricks.filter(t => t.name.toLowerCase().includes(normalize)
        || t.description.toLowerCase().includes(normalize))
    }
  },
}
</script>

<style scoped>

</style>
