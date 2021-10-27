<template>
<h2>Configuration</h2>
   <q-form
      @submit="onSubmit"
      @reset="onReset"
      class="q-gutter-md"
    >
      <q-input
        filled
        v-model="store.state.user.username"
        label="Username"
        hint="eg. Konrad"
        lazy-rules
        :rules="[ val => val && val.length > 0 || 'Please enter a username']"
      />

      <q-input
        filled
        v-model="store.state.user.password"
        type="password"
        label="Password"
        lazy-rules
        :rules="[
          val => val && val.length > 0 || 'Please enter a prefix for your container'
        ]"
      />

      <q-input
        filled
        v-model="store.state.user.course"
        label="Course"
        hint="eg. H3PD011121"
        lazy-rules
        :rules="[
          val => val !== null && val !== '' || 'Enter your course name',
        ]"
      />

      <div>
        <q-btn label="Save" type="submit" color="primary"/>
        &nbsp;
        <q-btn label="Back" to="/" color="grey"/>
      </div>
    </q-form>
</template>

<script setup>

//const PouchDB = require("pouchdb-browser").default;
//import Encryption from '../scripts/Encryption.js'
import store from '@/store'
import Database from '../scripts/Database.js'
//const $q = useQuasar()

/*const username = ref(null)
const password = ref(null)
const course = ref(null)*/

//var db = new PouchDB("constellation");

/*db.get('user')
  .then((doc) => {
    store.state.username = doc.username,
    store.state.password = doc.password,
    store.state.course = doc.course
    store.state.container_definitions = doc.container_definitions;
    store.state._rev = doc._rev;
  })
  .catch(() => {
    
    //Initialize container_definition with some example
    store.state.container_definitions = [{
        image : "hello-world",
        prefix : "hello-world"
      }];
  })*/

function onSubmit () {
  Database.saveData().then(
    () => {
      Database.LoadData();
      require("../router/index.js").default.push("/")
    }
  )
}

</script>

<style>

</style>