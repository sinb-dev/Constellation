<template>
<h2>{{store.username}}</h2>
   <q-form
      @submit="onSubmit"
      @reset="onReset"
      class="q-gutter-md"
    >
      <q-input
        filled
        v-model="store.state.username"
        label="Username"
        hint="eg. Konrad"
        lazy-rules
        :rules="[ val => val && val.length > 0 || 'Please enter a username']"
      />

      <q-input
        filled
        v-model="store.state.password"
        type="password"
        label="Password"
        lazy-rules
        :rules="[
          val => val && val.length > 0 || 'Please enter a prefix for your container'
        ]"
      />

      <q-input
        filled
        v-model="store.state.course"
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

const PouchDB = require("pouchdb-browser").default;
//import Encryption from '../scripts/Encryption.js'
import store from '@/store'

//const $q = useQuasar()

/*const username = ref(null)
const password = ref(null)
const course = ref(null)*/

var db = new PouchDB("constellation");

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
  saveUser()
}
function saveUser() 
{
  db.put({
    _id : "user",
    _rev : store.state._rev,
    username : store.state.username,
    password : store.state.password,
    course : store.state.course,
    container_definitions : store.state.container_definitions
  })

//  db.sync('constellation', 'https://localhost:5984/mydb');

}
</script>

<style>

</style>