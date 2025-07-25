# 3D Mental Rotation Test (3D-MRT)

A Unity-based adaptation of the standard paper-based Mental Rotation Test, providing an interactive high-fidelity 3D assessment of spatial reasoning abilities. 

This is not an exact replication but an similar spatial task that can be used to assess spatial reasoning skills. It is not statistically verified but a prototype for testing 3D spatial reasoning.

https://www.labvanced.com/content/research/en/blog/2024-05-3d-and-2d-mental-rotation-task/

## Overview

This test presents participants with 12 progressively complex 3D shapes constructed from cubic blocks. For each task, participants view a reference shape and must identify two correct rotated versions from four options. The system automatically tracks completion times and provides visual feedback for correct/incorrect selections.

<img width="1846" height="1153" alt="Mental rotation fig" src="https://github.com/user-attachments/assets/af896675-4efc-46a5-bea6-961913747074" />


## Key Features

- **Progressive Complexity**: Shape complexity increases across 12 tasks
- **Dual Correct Answers**: Two of four options are correct for each task
- **Automatic Timing**: System records total time and calculates average time per shape
- **Visual Feedback**: Green highlights for correct selections, red for incorrect
- **Two-Run Protocol**: Participants complete the test twice for reliability

## Limitations
- **Shapes are pre-difined**: Position of the shapes is randomised but they are currenly pre-made prefabs
- **Shapes is not automated**: Scoring method listed below is currenly requires manual assessment

## Scoring Method

The current version requires manual score calculation. Follow these steps:

1. **Record Completion Data**:
   - Total time for both runs (seconds)
   - Number of errors per run
   - Calculate average time per shape: `total_time / 12`

2. **Apply Time Penalties**:
   - For each error, add one average shape time to total time
   - Example: 44s total, 3.6s average per shape, 2 errors = 44 + (2 × 3.6) = 51.2s

3. **Calculate Final Score**:
   - Average the adjusted total times from both runs
   - Lower scores indicate better spatial reasoning performance
  
<img width="2481" height="1397" alt="mental rotation 12" src="https://github.com/user-attachments/assets/057c15ff-5545-433e-aaaf-6cb66fa23fbe" />


## Usage

Participants are informed that completion time is the primary scoring factor, but specific times and penalties are not displayed during testing to avoid performance anxiety.

**Note**: Automatic scoring will be implemented in a future version.
