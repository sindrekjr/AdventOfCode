use std::{
    collections::{HashMap, HashSet, VecDeque},
    iter::once,
};

use crate::{
    core::{Part, Solution},
    utils::grid::{parse_grid, Coordinate, Direction},
};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day16::solve_part_one(input),
        Part::P2 => Day16::solve_part_two(input),
    }
}

struct Day16;
impl Solution for Day16 {
    fn solve_part_one(input: String) -> Option<String> {
        let (start, end, walls) = parse_elements(&input);
        let width = walls.iter().max_by_key(|coor| coor.x).unwrap().x;
        let height = walls.iter().max_by_key(|coor| coor.y).unwrap().y;

        Some(
            dijkstra((start, Direction::E), walls, width, height)
                .0
                .iter()
                .filter_map(
                    |(&(coor, _), cost)| {
                        if coor == end {
                            Some(cost)
                        } else {
                            None
                        }
                    },
                )
                .min()
                .unwrap()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let (start, end, walls) = parse_elements(&input);
        let width = walls.iter().max_by_key(|coor| coor.x).unwrap().x;
        let height = walls.iter().max_by_key(|coor| coor.y).unwrap().y;
        let (visits, predecessors) = dijkstra((start, Direction::E), walls, width, height);
        let key = visits
            .iter()
            .filter_map(|(&key, _)| if key.0 == end { Some(key) } else { None })
            .collect::<Vec<_>>()[0];

        Some(predecessors[&key].len().to_string())
    }
}

fn parse_elements(input: &str) -> (Coordinate, Coordinate, HashSet<Coordinate>) {
    let map = parse_grid(input);
    let start = *map.iter().find(|(_, ch)| **ch == 'S').unwrap().0;
    let end = *map.iter().find(|(_, ch)| **ch == 'E').unwrap().0;
    let walls: HashSet<Coordinate> = map
        .iter()
        .filter_map(|(coor, ch)| match *ch == '#' {
            true => Some(*coor),
            false => None,
        })
        .collect();

    (start, end, walls)
}

fn dijkstra(
    start: (Coordinate, Direction),
    walls: HashSet<Coordinate>,
    width: isize,
    height: isize,
) -> (
    HashMap<(Coordinate, Direction), u32>,
    HashMap<(Coordinate, Direction), HashSet<Coordinate>>,
) {
    let mut queue: VecDeque<_> = [(start, 0)].into();
    let mut visited: HashMap<(Coordinate, Direction), u32> = [(start, 0)].into();
    let mut predecessors: HashMap<(Coordinate, Direction), HashSet<Coordinate>> =
        [(start, HashSet::new())].into();

    while let Some(((coor, dir), cost)) = queue.pop_front() {
        if let Some(&visit) = visited.get(&(coor, dir)) {
            if visit < cost {
                continue;
            }
        }

        let next = coor.neighbour_by_direction(&dir);
        if 0 <= next.x && next.x < width && 0 <= next.y && next.y < height && !walls.contains(&next)
        {
            let key = (next, dir);
            let new_cost = cost + 1;
            match visited.get(&key) {
                Some(visit) if &new_cost > visit => continue,
                Some(visit) if &new_cost == visit => {
                    let previous_predecessors = &predecessors[&(coor, dir)].clone();
                    predecessors
                        .entry(key)
                        .or_default()
                        .extend(previous_predecessors.into_iter().cloned().chain(once(coor)));
                }
                _ => {
                    predecessors.insert(
                        key,
                        predecessors[&(coor, dir)]
                            .clone()
                            .into_iter()
                            .chain(once(coor))
                            .collect(),
                    );

                    queue.push_back((key, new_cost));
                    visited.insert(key, new_cost);
                }
            }
        }

        for rotation in [dir.rotate_left_90deg(), dir.rotate_right_90deg()] {
            let key = (coor, rotation);
            let new_cost = cost + 1000;
            match visited.get(&key) {
                Some(visit) if &new_cost > visit => continue,
                Some(visit) if &new_cost == visit => {
                    let previous_predecessors = &predecessors[&(coor, dir)].clone();
                    predecessors
                        .entry(key)
                        .or_default()
                        .extend(previous_predecessors.into_iter().cloned().chain(once(coor)));
                }
                _ => {
                    predecessors.insert(
                        key,
                        predecessors[&(coor, dir)]
                            .clone()
                            .into_iter()
                            .chain(once(coor))
                            .collect(),
                    );

                    queue.push_back((key, new_cost));
                    visited.insert(key, new_cost);
                }
            }
        }
    }

    (visited, predecessors)
}
