import React, { useEffect, useRef } from 'react'
import * as THREE from 'three'

export default function LoadPlanner3D() {
  const mountRef = useRef(null)

  useEffect(() => {
    const width = mountRef.current.clientWidth
    const height = mountRef.current.clientHeight

    const scene = new THREE.Scene()
    const camera = new THREE.PerspectiveCamera(60, width / height, 1, 50000)
    camera.position.set(5000, 3000, 5000)

    const renderer = new THREE.WebGLRenderer({ antialias: true })
    renderer.setSize(width, height)
    mountRef.current.appendChild(renderer.domElement)

    // LUCI
    const light = new THREE.AmbientLight(0xffffff, 1)
    scene.add(light)

    // MEZZO TRASPARENTE (CAMION)
    const vehicleGeometry = new THREE.BoxGeometry(13600, 2700, 2450)
    const vehicleMaterial = new THREE.MeshBasicMaterial({
      color: 0x00aaff,
      transparent: true,
      opacity: 0.15,
      wireframe: false
    })
    const vehicleMesh = new THREE.Mesh(vehicleGeometry, vehicleMaterial)
    vehicleMesh.position.set(6800, 1350, 1225)
    scene.add(vehicleMesh)

    // LOTTO DEMO
    const itemGeometry = new THREE.BoxGeometry(1200, 1500, 800)
    const itemMaterial = new THREE.MeshBasicMaterial({ color: 0xff8800 })
    const itemMesh = new THREE.Mesh(itemGeometry, itemMaterial)
    itemMesh.position.set(600, 750, 400)
    scene.add(itemMesh)

    // CONTROLLI
    const animate = function () {
      requestAnimationFrame(animate)
      renderer.render(scene, camera)
    }
    animate()

    return () => {
      mountRef.current.removeChild(renderer.domElement)
    }
  }, [])

  return <div ref={mountRef} style={{ flex: 1 }} />
}
