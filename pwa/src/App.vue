<template>
  <q-layout view="hHh lpR fFf" >

    <q-header elevated class="bg-primary text-white">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar icon="view_in_ar" size="64px">
            <!--<img src="https://cdn.quasar.dev/logo-v2/svg/logo-mono-white.svg">-->
            
          </q-avatar>
          Constellation
        </q-toolbar-title>

        <q-btn dense flat round icon="menu" @click="toggleRightDrawer" />
      </q-toolbar>
    </q-header>

    <q-drawer show-if-above v-model="rightDrawerOpen" side="right" bordered>
      <!-- drawer content -->
    </q-drawer>

    <q-page-container class="fixed-center" style="padding-top:500px">
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