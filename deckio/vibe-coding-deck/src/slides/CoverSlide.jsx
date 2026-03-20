/**
 * SAMPLE CONTENT ONLY
 * This slide contains scaffolded placeholder copy.
 * Agents must not use it as factual project context until the user replaces it.
 */
import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './CoverSlide.module.css'

export default function CoverSlide() {
  return (
    <Slide index={0} className={styles.cover}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />
      <div className={`orb ${styles.orb3}`} />

      <div className="content-frame content-gutter">
        <div className={styles.content}>
          <p className={styles.eyebrow}>Vibe Coding Hackathon</p>
          <h1>
            Vibe Coding with<br />
            <span className={styles.highlight}>GitHub Copilot CLI</span>
          </h1>
          <p className={styles.subtitle}>
            Intro, capabilities, Tim&apos;s concrete case, hack expectations,
            prerequisites, and kickoff plan.
          </p>

          <div className={styles.meta}>
            <div className={styles.metaItem}>
              <span className={styles.metaLabel}>Audience</span>
              <span className={styles.metaValue}>Hackathon Teams</span>
            </div>
            <div className={styles.metaDivider} />
            <div className={styles.metaItem}>
              <span className={styles.metaLabel}>Focus</span>
              <span className={styles.metaValue}>Build fast, validate fast</span>
            </div>
          </div>
        </div>

        <div className={styles.watermark} aria-hidden="true">
          <img src="/deckio.png" alt="" className={styles.watermarkIcon} />
          <span className={styles.watermarkText}>DECKIO</span>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
