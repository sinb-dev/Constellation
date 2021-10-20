<template>
  <q-layout view="hHh lpR fFf" class="row justify-center" >

    <q-header elevated class="bg-primary text-white">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar icon="ion-square" size="64px">
            <!--<img src="https://cdn.quasar.dev/logo-v2/svg/logo-mono-white.svg">-->
            
          </q-avatar>
          Constellation
        </q-toolbar-title>

        <q-btn dense flat round icon="menu" @click="toggleRightDrawer" />
      </q-toolbar>
    </q-header>

    <q-drawer show-if-above v-model="rightDrawerOpen" side="right" bordered dark>
      <!-- drawer content -->
      
      more or less<br>more or less<br>
      more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>more or less<br>
      
    </q-drawer>

    <q-page-container class="col-md-8 col-sm-8 col-xs-12" >
      <router-view />
    </q-page-container>

  </q-layout>
</template>

<script setup>
//import store from '@/store'
import { ref } from 'vue'

const rightDrawerOpen = ref(false)

function toggleRightDrawer () {
  rightDrawerOpen.value = !rightDrawerOpen.value
}

</script>