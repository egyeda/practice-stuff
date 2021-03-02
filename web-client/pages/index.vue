<template>
  <v-row justify="center" align="center">
    <v-col cols="12" sm="8" md="6">
      <div class="text-center">
        <logo />
        <vuetify-logo />
      </div>
      <v-card>

        <v-card-title class="headline">
          {{message}}
          <v-btn @click="reset"> Reset </v-btn>
        </v-card-title>

        <div v-if="tricks">
          <p v-for="t in tricks">
            {{t.name}}
          </p>
        </div>
        <div>
          <v-text-field :label="'Trick name'" v-model="trickName"></v-text-field>
          <v-btn @click="saveTrick"> Save trick</v-btn>
          <v-btn @click="resetTricks"> Reset tricks </v-btn>
        </div>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import Logo from '~/components/Logo.vue'
import VuetifyLogo from '~/components/VuetifyLogo.vue'
import Axios from 'axios'
import {mapState, mapActions, mapMutations} from 'vuex'

export default {
  components: {
    Logo,
    VuetifyLogo
  },
  data:() => ({
    trickName: ""
  }),
  computed: {
    ...mapState({
    message: state => state.message
    }),
    ...mapState('tricks', {
      tricks: state => state.tricks
    })
  },
  methods: {
    ...mapMutations([
     'reset'
    ]),
    ...mapMutations('tricks', {
      resetTricks: 'reset'
    }),
    ...mapActions('tricks', ['createTricks']),
    async saveTrick(){
      await this.createTricks({trick: {name: this.trickName}});
      this.trickName = "";
    }
  }
  // async fetch (){
  //   await this.$store.dispatch('fetchMessage');
  // }
  // asyncData(payload){
  //   console.log("asyncData called");
  //   return Axios.get("http://localhost:5000/api/home")
  //     .then(({data}) => {
  //       return { message: data }
  //     })
  // }
}
</script>
